using CoreApi.Application.Common.Interfaces;
using CoreApi.Domain.Entities;
using CoreApi.Domain.Interfaces;
using MediatR;

namespace CoreApi.Application.Auth.Queries.ValidateUser;

public record ValidateUserQuery(string Email, string Password) : IRequest<User?>;

public class ValidateUserQueryHandler : IRequestHandler<ValidateUserQuery, User?>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public ValidateUserQueryHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User?> Handle(ValidateUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || user.PasswordHash == null)
        {
            return null;
        }

        if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
        {
            return null;
        }

        return user;
    }
}
