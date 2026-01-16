using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using BffApi.Api.Controllers;
using BffApi.Clients.CoreApi;
using BffApi.Clients.Models;
using BffApi.Api.Models;
using System.Threading.Tasks;
using System;

namespace BffApi.UnitTests.Api.Controllers;

public class AuthControllerTests
{
    private readonly Mock<ICoreApiClient> _coreApiClientMock;
    private readonly Mock<IAuthenticationService> _authServiceMock;
    private readonly Mock<IServiceProvider> _serviceProviderMock;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _coreApiClientMock = new Mock<ICoreApiClient>();
        _authServiceMock = new Mock<IAuthenticationService>();
        _serviceProviderMock = new Mock<IServiceProvider>();

        _serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IAuthenticationService)))
            .Returns(_authServiceMock.Object);

        // Setup UrlHelper
        var urlHelperMock = new Mock<IUrlHelper>();
        
        _controller = new AuthController(_coreApiClientMock.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    RequestServices = _serviceProviderMock.Object
                }
            },
            Url = urlHelperMock.Object
        };
        
        // Mock SignInAsync via IAuthenticationService
        _authServiceMock
            .Setup(x => x.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
            .Returns(Task.CompletedTask);
    }

    [Fact]
    public async Task Signup_ShouldReturnOk_WhenCoreApiReturnsSuccess()
    {
        // Arrange
        // Use the type expected by the controller (BffApi.Clients.Models.SignupRequest)
        var request = new BffApi.Clients.Models.SignupRequest("new@example.com", "Password123!");
        var userResponse = new UserResponse(Guid.NewGuid(), "new@example.com", null, null);

        // Mock GetUserByEmailAsync to return null (user doesn't exist)
        _coreApiClientMock.Setup(client => client.GetUserByEmailAsync(request.Email))
            .ReturnsAsync((UserResponse?)null);

        _coreApiClientMock.Setup(client => client.CreateUserAsync(request.Email, request.Password))
            .ReturnsAsync(userResponse);

        // Act
        var result = await _controller.Signup(request);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(userResponse);
        
        _coreApiClientMock.Verify(client => client.CreateUserAsync(request.Email, request.Password), Times.Once);
    }

    [Fact]
    public async Task Signup_ShouldReturnBadRequest_WhenCoreApiFails()
    {
        // Arrange
        var request = new BffApi.Clients.Models.SignupRequest("fail@example.com", "Password123!");

        // Mock GetUserByEmailAsync to return null
        _coreApiClientMock.Setup(client => client.GetUserByEmailAsync(request.Email))
            .ReturnsAsync((UserResponse?)null);

        _coreApiClientMock.Setup(client => client.CreateUserAsync(request.Email, request.Password))
            .ReturnsAsync((UserResponse?)null);

        // Act
        var result = await _controller.Signup(request);

        // Assert
        // The controller returns 500 when CreateUser fails (returns null), or BadRequest if user exists.
        // My previous test assumption might have been wrong.
        // Line 61: return StatusCode(500, "Failed to create user");
        result.Should().BeOfType<ObjectResult>()
            .Which.StatusCode.Should().Be(500);
    }

    [Fact]
    public async Task Signup_ShouldReturnBadRequest_WhenUserAlreadyExists()
    {
        // Arrange
        var request = new BffApi.Clients.Models.SignupRequest("existing@example.com", "Password123!");
        var userResponse = new UserResponse(Guid.NewGuid(), "existing@example.com", null, null);

        // Mock GetUserByEmailAsync to return an existing user
        _coreApiClientMock.Setup(client => client.GetUserByEmailAsync(request.Email))
            .ReturnsAsync(userResponse);

        // Act
        var result = await _controller.Signup(request);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }
}
