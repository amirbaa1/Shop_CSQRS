using AutoMapper;
using Identity.Api.Features.Commands.Login;
using Identity.Api.Features.Commands.Register;
using Identity.Domain.Models;

namespace Identity.Worker.Mapping;

public class AuthMapper : Profile
{
    public AuthMapper()
    {
        CreateMap<LoginModel, LoginCommand>().ReverseMap();
        CreateMap<RegisterModel, RegisterCommand>().ReverseMap();

        // CreateMap<ProfileQuery,>()
    }
}