using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Domain.Models;
using Identity.Domain.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.Repository;

public class TokenGenerator : ITokenGenerator
{
    private readonly JwtOption _jwtOption;

    public TokenGenerator(IOptions<JwtOption> jwtOption)
    {
        _jwtOption = jwtOption.Value;
    }

    public string GeneratorToken(AppUser appUser)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOption.Secret!);

        var claim = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, appUser.FirstName),
            new Claim(JwtRegisteredClaimNames.Name, appUser.LastName),
            new Claim(JwtRegisteredClaimNames.Name, appUser.UserName),
            new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
            new Claim(JwtRegisteredClaimNames.Sub, appUser.Id),
        };

        var tokenDescription = new SecurityTokenDescriptor
        {
            Audience = _jwtOption.Audience,
            Issuer = _jwtOption.Issuer,
            Subject = new ClaimsIdentity(claim),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);
    }
}