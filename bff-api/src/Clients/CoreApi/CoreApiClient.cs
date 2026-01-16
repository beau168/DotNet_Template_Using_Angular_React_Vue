using System.Net.Http.Json;
using BffApi.Clients.Models;
using Microsoft.Extensions.Configuration;

namespace BffApi.Clients.CoreApi;

public class CoreApiClient : ICoreApiClient
{
    private readonly HttpClient _httpClient;

    public CoreApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        var baseUrl = configuration["CoreApi:BaseUrl"] ?? "https://localhost:7001"; // Default to common HTTPS port or config
        _httpClient.BaseAddress = new Uri(baseUrl);
    }

    public async Task<UserResponse?> CreateUserAsync(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/users", new { Email = email, Password = password });
        if (!response.IsSuccessStatusCode) return null;
        
        // Assuming Core API returns { userId: guid } or similar, we might need to fetch the user or just return a mock response with ID
        // For now, let's just return a basic UserResponse if successful, or fetch details if possible.
        // Simplified: return what we know.
        var result = await response.Content.ReadFromJsonAsync<dynamic>();
        // Wait, dynamic is tricky. Let's assume we want to return the user object if possible.
        // If Create returns ID, we might need to fetch it.
        // Let's rely on GetUserByEmail for full details if needed, or adjust Core API to return User.
        // Core API 'Create' returns { UserId = ... }.
        // Let's just return a partial response for now or call GetUserByEmail.
        return await GetUserByEmailAsync(email);
    }

    public async Task<UserResponse?> ValidateUserAsync(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/users/validate", new { Email = email, Password = password });
        if (!response.IsSuccessStatusCode) return null;
        
        return await response.Content.ReadFromJsonAsync<UserResponse>();
    }

    public async Task<UserResponse?> GetUserByEmailAsync(string email)
    {
        var response = await _httpClient.GetAsync($"/api/users/by-email/{email}");
        if (!response.IsSuccessStatusCode) return null;
        
        return await response.Content.ReadFromJsonAsync<UserResponse>();
    }
}
