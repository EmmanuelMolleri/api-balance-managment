using PhoneFinancialManagment.Domain.Models;
using PhoneFinancialManagment.Domain.Services;
using StackExchange.Redis;

namespace PhoneFinancialManagment.Infraestructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConnectionMultiplexer _redis;

    public AuthenticationService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public UserClaims ValidateTokenAndGetUserClaims(string token)
    {
        var db = _redis.GetDatabase();
        var userId = db.StringGet($"auth:tokens:{token}");

        if (userId.IsNullOrEmpty)
        {
            return null;
        }

        var roles = db.SetMembers($"auth:users:{userId}:roles").Select(role => role.ToString()).ToList();

        return new UserClaims
        {
            UserId = userId,
            Roles = roles
        };
    }
}