using Xunit;
using Moq;
using FluentAssertions;
using CoreApi.Application.Auth.Commands.CreateUser;
using CoreApi.Domain.Interfaces;
using CoreApi.Application.Common.Interfaces;
using CoreApi.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace CoreApi.UnitTests.Application.Auth.Commands.CreateUser;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _passwordHasherMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateUser_WhenEmailIsUnique()
    {
        // Arrange
        var command = new CreateUserCommand("test@example.com", "Password123!");
        
        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(command.Email))
            .ReturnsAsync((User?)null);
            
        _passwordHasherMock.Setup(hasher => hasher.HashPassword(command.Password))
            .Returns("hashed_password");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        _userRepositoryMock.Verify(repo => repo.CreateAsync(It.Is<User>(u => 
            u.Email == command.Email && 
            u.PasswordHash == "hashed_password"
        )), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUserAlreadyExists()
    {
        // Arrange
        var command = new CreateUserCommand("existing@example.com", "Password123!");
        
        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(command.Email))
            .ReturnsAsync(new User { Email = command.Email });

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("User with this email already exists.");
    }
}
