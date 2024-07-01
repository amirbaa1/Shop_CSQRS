namespace Identity.Domain.Models;

public class JwtOption
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public string? Secret { get; set; }
} //go active Configuration in program.cs