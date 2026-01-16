using BffApi.Clients.Models;

namespace BffApi.Clients.CoreApi;

public interface ICoreApiClient
{
    Task<UserResponse?> ValidateUserAsync(string email, string password);
    Task<UserResponse?> CreateUserAsync(string email, string password);
    Task<UserResponse?> GetUserByEmailAsync(string email);
}
