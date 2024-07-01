using AutoMapper;
using Identity.Application.Features.Commands.Login;
using Identity.Application.Features.Commands.Register;
using Identity.Domain.Models;

namespace Identity.Application.Mapping;

public class AuthMapper : Profile
{
    public AuthMapper()
    {
        CreateMap<LoginModel, LoginCommand>().ReverseMap();
        CreateMap<RegisterModel, RegisterCommand>().ReverseMap();

        // CreateMap<ProfileQuery,>()
    }
}