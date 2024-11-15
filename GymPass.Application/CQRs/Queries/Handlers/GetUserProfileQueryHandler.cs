using GymPass.Application.CQRs.Queries.Requests;
using GymPass.Application.CQRs.Queries.Responses;
using GymPass.Domain.Repositories;
using MediatR;
using GymPass.Shared.Exceptions;

namespace GymPass.Application.CQRs.Queries.Handlers;

public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, GetUserProfileResponse>
{
    private readonly IUsersRepository _usersRepository;

    public GetUserProfileQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<GetUserProfileResponse> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.FindById(request.UserId);

        if (user is null)
        {
            throw new NotFoundRegisterException("Usuário não existe.");
        }

        return new GetUserProfileResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }

}
