using System.Collections.Concurrent;
using CoreApi.Domain.Entities;
using CoreApi.Domain.Interfaces;

namespace CoreApi.Infrastructure.Persistence;

public class InMemoryUserRepository : IUserRepository
{
    private static readonly ConcurrentDictionary<Guid, User> _users = new();
    private static readonly ConcurrentDictionary<string, User> _usersByEmail = new();
    private static readonly ConcurrentDictionary<string, RefreshToken> _refreshTokens = new();

    public Task<User?> GetByIdAsync(Guid id)
    {
        _users.TryGetValue(id, out var user);
        return Task.FromResult(user);
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        _usersByEmail.TryGetValue(email, out var user);
        return Task.FromResult(user);
    }

    public Task<User> CreateAsync(User user)
    {
        _users.TryAdd(user.Id, user);
        _usersByEmail.TryAdd(user.Email, user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        _users.AddOrUpdate(user.Id, user, (_, _) => user);
        _usersByEmail.AddOrUpdate(user.Email, user, (_, _) => user);
        return Task.CompletedTask;
    }

    public Task<RefreshToken?> GetRefreshTokenAsync(string tokenHash)
    {
        _refreshTokens.TryGetValue(tokenHash, out var token);
        return Task.FromResult(token);
    }

    public Task SaveRefreshTokenAsync(RefreshToken token)
    {
        _refreshTokens.AddOrUpdate(token.TokenHash, token, (_, _) => token);
        return Task.CompletedTask;
    }

    public Task RevokeRefreshTokenAsync(Guid tokenId)
    {
        var token = _refreshTokens.Values.FirstOrDefault(t => t.Id == tokenId);
        if (token != null)
        {
            token.RevokedAt = DateTime.UtcNow;
            _refreshTokens.AddOrUpdate(token.TokenHash, token, (_, _) => token);
        }
        return Task.CompletedTask;
    }
}
