using AutoMapper;
using CinemaGoEF.Application.Services.Interfaces;
using CinemaGoEF.Core.Enums;
using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.DTOs.UserDTOs;
using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Application.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IEmailService emailService,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _emailService = emailService;
        _mapper = mapper;
    }

    public async Task Register(CreateUserDto userDto)
    {
        if (string.IsNullOrWhiteSpace(userDto.Username))
        {
            throw new InvalidOperationException("Username is required.");
        }

        if (string.IsNullOrWhiteSpace(userDto.Email))
        {
            throw new InvalidOperationException("Email is required.");
        }

        if (string.IsNullOrWhiteSpace(userDto.Password))
        {
            throw new InvalidOperationException("Password is required.");
        }

        ICollection<User> users = await _userRepository.GetAllUsers();

        bool emailExists = users.Any(u => u.Email.ToLower() == userDto.Email.ToLower());

        if (emailExists)
        {
            throw new InvalidOperationException("Email already exists.");
        }

        bool usernameExists = users.Any(u => u.Username.ToLower() == userDto.Username.ToLower());

        if (usernameExists)
        {
            throw new InvalidOperationException("Username already exists.");
        }

        string verificationCode = Random.Shared.Next(1000, 10000).ToString();

        User user = new User
        {
            Username = userDto.Username,
            Email = userDto.Email,
            Password = _passwordHasher.HashPassword(userDto.Password),
            Balance = 0,
            Role = USER_ROLE.Customer,
            IsEmailVerified = false,
            VerificationCode = verificationCode,
            VerificationCodeExpiry = DateTime.Now.AddMinutes(5)
        };

        await _userRepository.AddUser(user);
        await _unitOfWork.SaveChangesAsync();

        await _emailService.SendEmail(
            user.Email,
            "CinemaGo Email Verification",
            $"Your verification code is: {verificationCode}");
    }

    public async Task VerifyRegistration(string email, string code)
    {
        ICollection<User> users = await _userRepository.GetAllUsers();

        User? user = users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        if (user.IsEmailVerified)
        {
            throw new InvalidOperationException("Email is already verified.");
        }

        if (user.VerificationCode != code)
        {
            throw new InvalidOperationException("Incorrect verification code.");
        }

        if (user.VerificationCodeExpiry == null || user.VerificationCodeExpiry < DateTime.Now)
        {
            throw new InvalidOperationException("Verification code has expired.");
        }

        user.IsEmailVerified = true;
        user.VerificationCode = null;
        user.VerificationCodeExpiry = null;

        await _userRepository.UpdateUser(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<UserDto?> Login(string email, string password)
    {
        ICollection<User> users = await _userRepository.GetAllUsers();

        User? user = users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

        if (user == null)
        {
            throw new InvalidOperationException("Invalid email or password.");
        }

        if (!user.IsEmailVerified)
        {
            throw new InvalidOperationException("Please verify your email first.");
        }

        bool passwordCorrect = _passwordHasher.VerifyPassword(password, user.Password);

        if (!passwordCorrect)
        {
            throw new InvalidOperationException("Invalid email or password.");
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> GetUserById(int id)
    {
        User user = await _userRepository.GetUserById(id);

        if (user == null)
        {
            return null;
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task<ICollection<UserDto>> GetAllUsers()
    {
        ICollection<User> users = await _userRepository.GetAllUsers();

        return _mapper.Map<ICollection<UserDto>>(users);
    }

    public async Task MakeAdmin(int userId)
    {
        User user = await _userRepository.GetUserById(userId);

        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        user.Role = USER_ROLE.Admin;

        await _userRepository.UpdateUser(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveAdmin(int userId)
    {
        User user = await _userRepository.GetUserById(userId);

        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        if (user.Role != USER_ROLE.Admin)
        {
            throw new InvalidOperationException("User is not an Admin.");
        }

        user.Role = USER_ROLE.Customer;

        await _userRepository.UpdateUser(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task AddBalance(int userId, decimal amount)
    {
        if (amount <= 0)
        {
            throw new InvalidOperationException("Amount must be greater than zero.");
        }

        User user = await _userRepository.GetUserById(userId);

        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        user.Balance += amount;

        await _userRepository.UpdateUser(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<decimal> GetBalance(int userId)
    {
        User user = await _userRepository.GetUserById(userId);

        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        return user.Balance;
    }
    
    public async Task UpdateUser(int id, UpdateUserDto userDto)
    {
        User user = await _userRepository.GetUserById(id);

        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        ICollection<User> users = await _userRepository.GetAllUsers();

        bool emailExists = users.Any(u => u.Id != id && u.Email.ToLower() == userDto.Email.ToLower());

        if (emailExists)
        {
            throw new InvalidOperationException("Email already exists.");
        }

        bool usernameExists = users.Any(u => u.Id != id && u.Username.ToLower() == userDto.Username.ToLower());

        if (usernameExists)
        {
            throw new InvalidOperationException("Username already exists.");
        }

        user.Username = userDto.Username;
        user.Email = userDto.Email;

        await _userRepository.UpdateUser(user);
        await _unitOfWork.SaveChangesAsync();
    }
}
