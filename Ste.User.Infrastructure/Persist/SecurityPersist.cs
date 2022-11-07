using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Ste.Framework.Common;
using Ste.User.Domain;
using Ste.User.Domain.Interface;

namespace Ste.User.Infrastructure.Persist;

public class SecurityPersist : ISecurityPersist
{

    private readonly IDbConnection _connection;
    private IDistributedCache _cache;

    public SecurityPersist(IDistributedCache cache, IDbConnection connection)
    {
        _cache = cache;
        _connection = connection;
    }

    public async Task<Token> CreateToken(AccessToken accessToken)
    {
        var refreshTokenId = Guid.NewGuid();
        var accessTokenId = Guid.NewGuid();
        accessToken.Id = accessTokenId;
        var token = new Token
        {
            AccessToken = SymmetricEncryption.Encrypt(accessTokenId + "", "TokenAes"),
            RefreshToken = SymmetricEncryption.Encrypt(refreshTokenId + "", "TokenAes"),
            ExpireAt = DateTime.Now.AddMinutes(60)
        };
        if (accessToken.User != null && accessToken.Ip != null)
            await AddRefreshToken(new RefreshToken
            {
                RefreshTokenId = refreshTokenId,
                AccessTokenId = accessTokenId,
                ExpireAt = DateTime.Now.AddDays(100),
                UserId = accessToken.User.UserId,
                Ip = accessToken.Ip
            });
        await _cache.SetAsync(accessTokenId + "", accessToken, new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(60)
        });
        return token;
    }

    public async Task RemoveToken(Guid refreshToken)
    {
        var tokenResult = await GetRefreshToken(refreshToken);
        if (tokenResult.Success)
        {
            await RemoveRefreshToken(refreshToken);
            await _cache.RemoveAsync(tokenResult.Data?.AccessTokenId + "");
        }
    }

    public async Task<Result> AddRefreshToken(RefreshToken refreshToken)
    {
        await _connection.InsertAsync(refreshToken);
        return new Success();
    }

    public async Task<Result> RemoveRefreshToken(Guid refreshTokenId)
    {
        await _connection.ExecuteAsync(
            "Delete From RefreshToken Where refreshTokenId=@refreshTokenId",
            new { refreshTokenId });
        return new Success();
    }

    public async Task<Result<RefreshToken>> GetRefreshToken(Guid refreshTokenId)
    {
        return await _connection.QuerySingleOrDefaultAsync(
            "Select * From RefreshToken Where refreshTokenId=@refreshTokenId",
            new { refreshTokenId });
    }

    public async Task<Result> UpdateAccessToken(Guid refreshTokenId, Guid accessTokenId)
    {
        return await _connection.QuerySingleOrDefaultAsync(
            "Update RefreshToken Set accessTokenId=@accessTokenId Where refreshTokenId=@refreshTokenId",
            new { refreshTokenId, accessTokenId });
    }
}