using CoreApi.Domain.Entities;
using CoreApi.Domain.Interfaces;
using MediatR;

namespace CoreApi.Application.Auth.Queries.GetUserByEmail;

public record GetUserByEmailQuery(string Email) : IRequest<User?>;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<User?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        return _userRepository.GetByEmailAsync(request.Email);
    }
}
