using CoreApi.Domain.Entities;

namespace CoreApi.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User> CreateAsync(User user);
    Task UpdateAsync(User user);
    
    Task<RefreshToken?> GetRefreshTokenAsync(string tokenHash);
    Task SaveRefreshTokenAsync(RefreshToken token);
    Task RevokeRefreshTokenAsync(Guid tokenId);
}
