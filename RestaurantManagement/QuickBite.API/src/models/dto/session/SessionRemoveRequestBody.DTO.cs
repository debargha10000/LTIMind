using System.ComponentModel.DataAnnotations;

namespace QuickBite.API.src.models.dto.session;

public class SessionRemoveRequestBodyDTO
{
    [Required(ErrorMessage = "Refresh token is required")]
    public required string RefreshToken { get; set; }

    // [SetsRequiredMembers]
    // public SessionRefreshRequestBodyDTO(string refreshToken)
    // {
    //     this.RefreshToken = refreshToken;
    // }
}
