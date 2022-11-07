using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;

namespace Ste.Framework.Common;

public interface IUserContextService
{
    Task<AccessToken?> GetToken();
}

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDistributedCache _cache;

    public UserContextService(IHttpContextAccessor httpContextAccessor, IDistributedCache cache)
    {
        _httpContextAccessor = httpContextAccessor;
        _cache = cache;
    }

    public async Task<AccessToken?> GetToken()
    {
        if (!_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("token"))
        {
            _httpContextAccessor.HttpContext?.Items.Add("AccessToken", null);
        }
        if (_httpContextAccessor.HttpContext != null && !_httpContextAccessor.HttpContext.Items.ContainsKey("AccessToken"))
        {
            _httpContextAccessor.HttpContext?.Items.Add("AccessToken", await _cache.GetAsync<AccessToken>(
                SymmetricEncryption.Decrypt(_httpContextAccessor.HttpContext.Request.Headers["token"], "TokenAes"))
            );
        }
        return _httpContextAccessor.HttpContext?.Items["AccessToken"] as AccessToken;
    }
}