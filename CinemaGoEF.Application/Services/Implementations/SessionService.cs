using AutoMapper;
using CinemaGoEF.Application.Services.Interfaces;
using CinemaGoEF.Core.Enums;
using CinemaGoEF.Core.Exceptions;
using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.DTOs.SessionDTOs;
using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Application.Services.Implementations;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly IHallRepository _hallRepository;
    private readonly IBookingService _bookingService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SessionService(
        ISessionRepository sessionRepository,
        IMovieRepository movieRepository,
        IHallRepository hallRepository,
        IBookingService bookingService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _sessionRepository = sessionRepository;
        _movieRepository = movieRepository;
        _hallRepository = hallRepository;
        _bookingService = bookingService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ICollection<SessionDto>> GetAllSessions()
    {
        ICollection<Session> sessions =await _sessionRepository.GetAllSessions();

        return _mapper.Map<ICollection<SessionDto>>(sessions);
    }

    public async Task<SessionDto?> GetSessionById(int id)
    {
        Session? session =await _sessionRepository.GetSessionById(id);

        if (session == null)
        {
            return null;
        }

        return _mapper.Map<SessionDto>(session);
    }

    public async Task<ICollection<SessionDto>> FilterSessions(int? movieId, DateTime? date)
    {
        ICollection<Session> sessions = await _sessionRepository.GetAllSessions();

        IEnumerable<Session> result = sessions.Where(session => session.Status == SESSION_STATUS.Active);

        if (movieId.HasValue)
        {
            result = result.Where(session => session.MovieId == movieId.Value);
        }

        if (date.HasValue)
        {
            result = result.Where(session => session.StartTime.Date == date.Value.Date);
        }

        result = result.OrderBy(session => session.StartTime);

        return _mapper.Map<ICollection<SessionDto>>(result.ToList());
    }

    public async Task<SessionDto> CreateSession(CreateSessionDto sessionDto)
    {
        Movie? movie = await _movieRepository.GetMovieById(sessionDto.MovieId);

        if (movie == null)
        {
            throw new NotFoundException("Movie not found.");
        }

        Hall? hall = await _hallRepository.GetById(sessionDto.HallId);

        if (hall == null)
        {
            throw new NotFoundException("Hall not found.");
        }

        if (sessionDto.StartTime <= DateTime.Now)
        {
            throw new ValidationException("Session time must be in the future.");
        }

        if (sessionDto.StandardPrice <= 0 || sessionDto.VipPrice <= 0)
        {
            throw new ValidationException("Ticket prices must be greater than 0.");
        }

        await CheckSessionOverlap(sessionDto.HallId, sessionDto.StartTime, movie.Duration);

        Session session = _mapper.Map<Session>(sessionDto);

        session.Status = SESSION_STATUS.Active;

        await _sessionRepository.AddSession(session);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<SessionDto>(session);
    }

    public async Task<SessionDto?> UpdateSession(int id, UpdateSessionDto sessionDto)
    {
        Session? session = await _sessionRepository.GetSessionById(id);

        if (session == null)
        {
            return null;
        }

        if (session.StartTime <= DateTime.Now)
        {
            throw new CinemaGoException("A session that has already started cannot be updated.");
        }

        if (sessionDto.StartTime <= DateTime.Now)
        {
            throw new ValidationException("Session time must be in the future.");
        }

        if (sessionDto.StandardPrice <= 0 || sessionDto.VipPrice <= 0)
        {
            throw new ValidationException("Ticket prices must be greater than 0.");
        }

        Movie? movie = await _movieRepository.GetMovieById(session.MovieId);

        if (movie == null)
        {
            throw new NotFoundException("Movie not found.");
        }

        if (sessionDto.Status == SESSION_STATUS.Active)
        {
            await CheckSessionOverlap(session.HallId, sessionDto.StartTime, movie.Duration, id);
        }

        _mapper.Map(sessionDto, session);

        await _sessionRepository.UpdateSession(session);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<SessionDto>(session);
    }

    public async Task<bool> CancelSession(int id)
    {
        Session? session = await _sessionRepository.GetSessionById(id);

        if (session == null)
        {
            throw new NotFoundException("Session not found.");
        }

        if (session.Status == SESSION_STATUS.Cancelled)
        {
            throw new CinemaGoException("Session is already cancelled.");
        }

        if (session.StartTime <= DateTime.Now)
        {
            throw new CinemaGoException("A session that has already started cannot be cancelled.");
        }

        await _bookingService.CancelBookingsForSession(id);

        session.Status = SESSION_STATUS.Cancelled;

        await _sessionRepository.UpdateSession(session);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    private async Task CheckSessionOverlap(int hallId, DateTime startTime, int movieDuration, int? excludedSessionId = null)
    {
        DateTime newSessionEnd = startTime.AddMinutes(movieDuration);

        ICollection<Session> sessions = await _sessionRepository.GetAllSessions();

        foreach (Session existingSession in sessions)
        {
            if (existingSession.HallId != hallId)
            {
                continue;
            }

            if (existingSession.Status != SESSION_STATUS.Active)
            {
                continue;
            }

            if (excludedSessionId.HasValue && existingSession.Id == excludedSessionId.Value)
            {
                continue;
            }

            Movie? existingMovie = await _movieRepository.GetMovieById(existingSession.MovieId);

            if (existingMovie == null)
            {
                continue;
            }

            DateTime existingSessionEnd = existingSession.StartTime.AddMinutes(existingMovie.Duration);

            bool overlaps = startTime < existingSessionEnd && newSessionEnd > existingSession.StartTime;
            
            if (overlaps)
            {
                throw new CinemaGoException("This hall is already occupied during this time.");
            }
        }
    }
}