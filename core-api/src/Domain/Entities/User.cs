using CoreApi.Domain.Enums;

namespace CoreApi.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = string.Empty;
    public bool EmailVerified { get; set; }
    public string? PasswordHash { get; set; }
    public AuthProvider AuthProvider { get; set; }
    public string? ProviderUserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }
    public bool IsActive { get; set; } = true;
}
