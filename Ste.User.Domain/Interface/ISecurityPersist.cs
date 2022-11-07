using Ste.Framework.Common;

namespace Ste.User.Domain.Interface;

public interface ISecurityPersist
{
    Task<Result> AddRefreshToken(RefreshToken refreshToken);
    Task<Result> RemoveRefreshToken(Guid refreshTokenId);
    Task<Result<RefreshToken>> GetRefreshToken(Guid refreshTokenId);
    Task<Result> UpdateAccessToken(Guid refreshTokenId, Guid accessTokenId);
    Task<Token> CreateToken(AccessToken accessToken);
    Task RemoveToken(Guid refreshToken);
}