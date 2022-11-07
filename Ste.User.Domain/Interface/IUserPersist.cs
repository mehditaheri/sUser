namespace Ste.User.Domain.Interface;

public interface IUserPersist
{
    Task<UserInformation?> GetUserInformation(string username);
}
