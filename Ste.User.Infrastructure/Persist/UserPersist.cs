using System.Data;
using Dapper;
using Ste.User.Domain;
using Ste.User.Domain.Interface;

namespace Ste.User.Infrastructure.Persist;

public class UserPersist : IUserPersist
{

    private readonly IDbConnection _connection;

    public UserPersist(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<UserInformation?> GetUserInformation(string username)
    {
        return await _connection.QuerySingleOrDefaultAsync<UserInformation>("Select * From UserInformation Where username=@username",
            new { username });
    }

}