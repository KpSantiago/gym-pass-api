using DDDApplication.Application.CQRs.Queries.Requests;
using DDDApplication.Application.CQRs.Queries.Responses;
using Domain.Repositories;
using MediatR;
using Shared.Exceptions;

namespace DDDApplication.Application.CQRs.Queries.Handlers;

public class FetchUserCheckInsHistoryQueryHandler : IRequestHandler<FetchUserCheckInsHistoryQuery, FetchUserCheckInsHistoryResponse>
{
    private readonly ICheckInsRepository _checkInsRepository;
    private readonly IUsersRepository _usersRepository;

    public FetchUserCheckInsHistoryQueryHandler(ICheckInsRepository checkInsRepository, IUsersRepository usersRepository)
    {
        _checkInsRepository = checkInsRepository;
        _usersRepository = usersRepository;
    }

    public async Task<FetchUserCheckInsHistoryResponse> Handle(FetchUserCheckInsHistoryQuery request, CancellationToken cancellationToken)
    {
        var doesUserExist = await _usersRepository.FindById(request.UserId);

        if (doesUserExist is null)
        {
            throw new NotFoundRegisterException("Usuário não existe.");
        }

        var checkIns = await _checkInsRepository.FindManyByUserId(request.UserId, request.Page == null || request.Page == 0 ? 1 : request.Page.Value);

        return new FetchUserCheckInsHistoryResponse
        {
            CheckIns = checkIns
        };
    }
}
