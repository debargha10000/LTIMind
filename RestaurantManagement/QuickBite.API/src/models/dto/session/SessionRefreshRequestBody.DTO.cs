using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace QuickBite.API.src.models.dto.session;

public class SessionRefreshRequestBodyDTO
{
    [Required(ErrorMessage = "Refresh token is required")]
    public required string RefreshToken { get; set; }

    // [SetsRequiredMembers]
    // public SessionRefreshRequestBodyDTO(string refreshToken)
    // {
    //     this.RefreshToken = refreshToken;
    // }
}
