using System.Security.Claims;
using BffApi.Clients.Models;
using BffApi.Clients.CoreApi;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BffApi.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ICoreApiClient _coreApiClient;

    public AuthController(ICoreApiClient coreApiClient)
    {
        _coreApiClient = coreApiClient;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _coreApiClient.ValidateUserAsync(request.Email, request.Password);
        if (user == null)
        {
            return Unauthorized(new { Error = "Invalid credentials" });
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddDays(7)
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        return Ok(user);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest request)
    {
        var existing = await _coreApiClient.GetUserByEmailAsync(request.Email);
        if (existing != null)
        {
            return BadRequest(new { Error = "User already exists" });
        }

        var user = await _coreApiClient.CreateUserAsync(request.Email, request.Password);
        if (user == null)
        {
            return StatusCode(500, "Failed to create user");
        }

        // Auto-login after signup
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        return Ok(user);
    }

    [HttpGet("login/{provider}")]
    public IActionResult LoginWithProvider(string provider)
    {
        // Scaffold: In a real app, this would build the provider's auth URL and redirect.
        // For local-first/mock, we could redirect to a mock page or just simulate a callback.
        // Example logic:
        // var redirectUrl = _oauthService.GetAuthorizationUrl(provider, "http://localhost:5002/api/auth/callback/" + provider);
        // return Redirect(redirectUrl);

        return Ok(new { Message = $"Initiating login with {provider} (Scaffold implementation)" });
    }

    [HttpGet("callback/{provider}")]
    public async Task<IActionResult> AuthCallback(string provider, [FromQuery] string code)
    {
        // Scaffold: Handle code exchange, get profile, sync with Core API.
        // var userProfile = await _oauthService.ExchangeCodeAsync(provider, code);
        // var user = await _coreApiClient.GetOrCreateExternalUserAsync(provider, userProfile.Id, userProfile.Email);
        
        // Auto-login logic (same as Signup/Login)
        // ...

        return Ok(new { Message = $"Callback from {provider} received with code {code} (Scaffold implementation)" });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new { Message = "Logged out" });
    }

    [HttpGet("me")]
    public IActionResult Me()
    {
        if (User.Identity?.IsAuthenticated != true)
        {
            return Unauthorized();
        }

        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return Ok(new { Id = id, Email = email });
    }
}
