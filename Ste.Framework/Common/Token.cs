namespace Ste.Framework.Common;


public enum LoginTypeSet{Internal = 1,Sso = 2,Government = 3}

[Serializable]
public class AccessToken
{
    public Guid Id { get; set; }
    public TokenUserInformation? User { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime ExpireAt { get; set; }
    public string? Ip { get; set; }
    public LoginTypeSet LoginType { get; set; }
}

public class Token
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime ExpireAt { get; set; }
}