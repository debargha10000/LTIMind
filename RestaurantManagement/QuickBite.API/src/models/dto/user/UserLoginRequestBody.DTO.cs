namespace QuickBite.API.src.models.dto.user;

public class UserLoginRequestBodyDTO
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
