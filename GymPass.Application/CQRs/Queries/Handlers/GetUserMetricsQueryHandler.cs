using GymPass.Application.CQRs.Queries.Requests;
using GymPass.Application.CQRs.Queries.Responses;
using GymPass.Domain.Repositories;
using GymPass.Shared.Exceptions;
using MediatR;

namespace GymPass.Application.CQRs.Queries.Handlers;

public class GetUserMetricsQueryHandler : IRequestHandler<GetUserMetricsQuery, GetUserMetricsResponse>
{
    private readonly ICheckInsRepository _checkInsRepository;
    private readonly IUsersRepository _usersRepository;

    public GetUserMetricsQueryHandler(ICheckInsRepository checkInsRepository, IUsersRepository usersRepository)
    {
        _checkInsRepository = checkInsRepository;
        _usersRepository = usersRepository;
    }

    public async Task<GetUserMetricsResponse> Handle(GetUserMetricsQuery request, CancellationToken cancellationToken)
    {
        var doesUserExist = await _usersRepository.FindById(request.UserId);

        if (doesUserExist is null)
        {
            throw new NotFoundRegisterException("Usuário não existe.");
        }
        
        int metrics = await _checkInsRepository.CountCheckInsByUserId(request.UserId);

        return new GetUserMetricsResponse {
            Id = request.UserId,
            Metrics = metrics
        };
    }
}
