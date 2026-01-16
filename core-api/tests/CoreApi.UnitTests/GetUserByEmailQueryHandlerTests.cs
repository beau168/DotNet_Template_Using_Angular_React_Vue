using Xunit;
using Moq;
using FluentAssertions;
using CoreApi.Application.Auth.Queries.GetUserByEmail;
using CoreApi.Domain.Interfaces;
using CoreApi.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CoreApi.UnitTests.Application.Auth.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly GetUserByEmailQueryHandler _handler;

    public GetUserByEmailQueryHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _handler = new GetUserByEmailQueryHandler(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var query = new GetUserByEmailQuery("test@example.com");
        var user = new User { Email = "test@example.com" };

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(query.Email))
            .ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        var query = new GetUserByEmailQuery("unknown@example.com");

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(query.Email))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
}
