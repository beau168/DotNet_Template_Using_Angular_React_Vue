namespace BffApi.Clients.Models;

public record LoginRequest(string Email, string Password);
public record SignupRequest(string Email, string Password);
public record UserResponse(Guid Id, string Email, string? FirstName, string? LastName);
