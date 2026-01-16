using Xunit;
using Moq;
using FluentAssertions;
using CoreApi.Application.Auth.Queries.ValidateUser;
using CoreApi.Domain.Interfaces;
using CoreApi.Application.Common.Interfaces;
using CoreApi.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CoreApi.UnitTests.Application.Auth.Queries.ValidateUser;

public class ValidateUserQueryHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly ValidateUserQueryHandler _handler;

    public ValidateUserQueryHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _handler = new ValidateUserQueryHandler(_userRepositoryMock.Object, _passwordHasherMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnUser_WhenCredentialsAreValid()
    {
        // Arrange
        var query = new ValidateUserQuery("test@example.com", "Password123!");
        var user = new User { Email = query.Email, PasswordHash = "hashed_pw" };

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(query.Email))
            .ReturnsAsync(user);
        
        _passwordHasherMock.Setup(x => x.VerifyPassword(query.Password, user.PasswordHash))
            .Returns(true);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be(query.Email);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenUserNotFound()
    {
        // Arrange
        var query = new ValidateUserQuery("unknown@example.com", "Password123!");

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(query.Email))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenPasswordIsInvalid()
    {
        // Arrange
        var query = new ValidateUserQuery("test@example.com", "WrongPassword");
        var user = new User { Email = query.Email, PasswordHash = "hashed_pw" };

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(query.Email))
            .ReturnsAsync(user);

        _passwordHasherMock.Setup(x => x.VerifyPassword(query.Password, user.PasswordHash))
            .Returns(false);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
}
