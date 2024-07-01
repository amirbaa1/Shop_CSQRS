using Identity.Domain.Models;
using Identity.Domain.Models.Dto;
using Identity.Domain.Repository;
using Identity.Infrastructure.Data;
using IdentityServer4.Extensions;
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

    public async Task<ResponseDto> ProfileService(string id)
    {
        var user = await _context.AppUsers.SingleOrDefaultAsync(x => x.Id == id);
        if (user == null)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "NotFound!",
                Result = null
            };
        }

        // var userProfile = new UserDto
        // {
        //     ID = user.Id,
        //     UserName = user.UserName,
        //     FirstName = user.FirstName,
        //     LastName = user.LastName,
        //     Email = user.Email,
        //     Image = user.Image,
        //     PhoneNumber = user.PhoneNumber,
        //     Role = user.Role,
        //     
        // };

        var userProfile = new AppUser
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Image = user.Image,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            TwoFactorEnabled = user.TwoFactorEnabled
        };

        return new ResponseDto
        {
            IsSuccess = true,
            Message = "ProfileService",
            Result = userProfile
        };
    }

    public async Task<ResponseDto> UpdateProfile(string id, UpdateProfile updateProfiles)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
        {
            return new ResponseDto

            {
                IsSuccess = false,
                Message = "NotFound!",
                Result = null
            };
        }

        if ((user.LastName == "string" || string.IsNullOrWhiteSpace(user.LastName)) &&
            (user.Image == "string" || string.IsNullOrWhiteSpace(user.Image)))
        {
            user.FirstName = updateProfiles.FirstName;
        }

        if ((user.FirstName == "string" || string.IsNullOrWhiteSpace(user.FirstName)) &&
            (user.Image == "string" || string.IsNullOrWhiteSpace(user.Image)))
        {
            user.LastName = updateProfiles.LastName;
        }

        if ((user.FirstName == "string" || string.IsNullOrWhiteSpace(user.FirstName)) &&
            (user.LastName == "string" || string.IsNullOrWhiteSpace(user.LastName)))
        {
            user.Image = updateProfiles.Image;
        }

        if (user.FirstName == "string" || string.IsNullOrWhiteSpace(user.FirstName))
        {
            user.LastName = updateProfiles.LastName;
            user.Image = updateProfiles.Image;
        }

        if (user.LastName == "string" || string.IsNullOrWhiteSpace(user.LastName))
        {
            user.FirstName = updateProfiles.FirstName;
            user.Image = updateProfiles.Image;
        }

        if (user.Image == "string" || string.IsNullOrWhiteSpace(user.Image))
        {
            user.FirstName = updateProfiles.FirstName;
            user.LastName = updateProfiles.LastName;
        }

        // void UpdateIfDefault(ref string userProperty, string updateProperty)
        // {
        //     if (userProperty == "string" || string.IsNullOrWhiteSpace(userProperty))
        //     {
        //         userProperty = updateProperty;
        //     }
        // }
        //
        // UpdateIfDefault(ref user.FirstName, updateProfiles.FirstName);
        // UpdateIfDefault(ref user.LastName, updateProfiles.LastName);
        // UpdateIfDefault(ref user.Image, updateProfiles.Image);
        //
        // var result = await _userManager.UpdateAsync(user);
        // if (result.Succeeded)
        // {
        //     return new ResponseDto
        //     {
        //         IsSuccess = true,
        //         Message = "Profile updated successfully!",
        //         Result = user
        //     };
        // }
        // else
        // {
        //     return new ResponseDto
        //     {
        //         IsSuccess = false,
        //         Message = "Update failed!",
        //         Result = result.Errors
        //     };
        // }


        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return new ResponseDto
            {
                IsSuccess = true,
                Message = "Profile updated successfully!",
                Result = user
            };
        }
        else
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "Update failed!",
                Result = result.Errors
            };
        }
    }

    //
    // public Task<string> SendActivateEmail(string userId)
    // {
    //     throw new NotImplementedException();
    // }
}