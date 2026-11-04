using AutoMapper;
using CinemaGoEF.Core.Models.DTOs.UserDTOs;
using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Admin, UserDto>();
        CreateMap<UserDto, Admin>();
        CreateMap<Customer, UserDto>();
        CreateMap<UserDto, Customer>();
        CreateMap<SuperUser, UserDto>();
        CreateMap<UserDto, SuperUser>();

        CreateMap<CreateUserDto, Admin>();
        CreateMap<Admin, CreateUserDto>();
        CreateMap<CreateUserDto, Customer>();
        CreateMap<Customer, CreateUserDto>();
        CreateMap<CreateUserDto, SuperUser>();
        CreateMap<SuperUser, CreateUserDto>();

        CreateMap<UpdateUserDto, Admin>();
        CreateMap<Admin, UpdateUserDto>();
        CreateMap<UpdateUserDto, Customer>();
        CreateMap<Customer, UpdateUserDto>();
        CreateMap<UpdateUserDto, SuperUser>();
        CreateMap<SuperUser, UpdateUserDto>();
    }
}