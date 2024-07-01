using Identity.Domain.Models;
using Identity.Domain.Models.Dto;
using Identity.Domain.Repository;
using Identity.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly AuthDbContext _context;
    private readonly ILogger<AuthDbContext> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenGenerator _tokenGenerator;

    public AuthRepository(AuthDbContext context, ILogger<AuthDbContext> logger, UserManager<AppUser> userManager,
        ITokenGenerator tokenGenerator)
    {
        _context = context;
        _logger = logger;
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<string> Register(RegisterModel register)
    {
        try
        {
            var user = new AppUser
            {
                UserName = register.UserName,
                Email = register.Email,
                FirstName = register.Name,
                LastName = "NoN",
                PhoneNumber = register.PhoneNumber,
                NormalizedEmail = register.Email.ToUpper(),
                Role = string.IsNullOrEmpty(register.Role) ? "user" : register.Role,
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                var userProfile = new UserDto
                {
                    ID = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.FirstName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role,
                };

                //Confirmed email
                // var createConfirmationTokenAsync = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);

                return "Create Register";
            }
            else
            {
                return result.Errors.FirstOrDefault()!.Description;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in code :{ex.Message}");

            return ex.Message;
        }
    }

    public async Task<LoginResponseDto> Login(LoginModel login)
    {
        var userLogin = await _context.AppUsers.SingleOrDefaultAsync(x => x.UserName == login.UserName);
        var userPass = await _userManager.CheckPasswordAsync(userLogin, login.Password);
        if (userPass == false)
        {
            return new LoginResponseDto
            {
                userDto = null,
                Token = ""
            };
        }

        var createToken = _tokenGenerator.GeneratorToken(userLogin);
        var profile = new UserDto
        {
            ID = userLogin.Id,
            UserName = userLogin.UserName,
            FirstName = userLogin.FirstName,
            LastName = userLogin.LastName,
            Image = userLogin.Image,
            Email = userLogin.Email,
            PhoneNumber = userLogin.PhoneNumber,
            Role = userLogin.Role
        };

        return new LoginResponseDto
        {
            userDto = profile,
            Token = createToken
        };
    }

    // public Task<ResponseDto> ProfileService(string Id)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task<ResponseDto> UpdateProfile(string id, UpdateProfile updateProfiles)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task<string> SendActivateEmail(string userId)
    // {
    //     throw new NotImplementedException();
    // }
}