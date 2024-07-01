namespace Identity.Domain.Models.Dto;

public class LoginResponseDto
{
    public UserDto userDto { get; set; }
    public string Token { get; set; }
}