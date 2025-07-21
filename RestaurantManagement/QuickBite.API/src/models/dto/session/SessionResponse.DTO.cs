using System.Diagnostics.CodeAnalysis;

namespace QuickBite.API.src.models.dto.session;

public class SessionResponseDTO
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }

    [SetsRequiredMembers]
    public SessionResponseDTO(string accessToken, string refreshToken)
    {
        this.AccessToken = accessToken;
        this.RefreshToken = refreshToken;
    }
}
