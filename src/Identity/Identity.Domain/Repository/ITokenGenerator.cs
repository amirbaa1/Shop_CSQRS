using Identity.Domain.Models;

namespace Identity.Domain.Repository;

public interface ITokenGenerator
{
    string GeneratorToken(AppUser appUser);
}