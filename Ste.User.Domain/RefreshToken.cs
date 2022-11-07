using Dapper.Contrib.Extensions;

namespace Ste.User.Domain;

[Table("RefreshToken")]
public class RefreshToken
{
    public Guid RefreshTokenId { get; set; }
    public Guid AccessTokenId { get; set; }
    public DateTime ExpireAt { get; set; }
    public int UserId { get; set; }
    public string Ip { get; set; }
}