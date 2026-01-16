using CoreApi.Application.Common.Interfaces;
using CoreApi.Domain.Entities;
using CoreApi.Domain.Enums;
using CoreApi.Domain.Interfaces;
using MediatR;

namespace CoreApi.Application.Auth.Commands.CreateUser;

public record CreateUserCommand(string Email, string Password) : IRequest<Guid>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new Exception("User with this email already exists.");
        }

        var user = new User
        {
            Email = request.Email,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            AuthProvider = AuthProvider.Local,
            EmailVerified = false
        };

        await _userRepository.CreateAsync(user);

        return user.Id;
    }
}
