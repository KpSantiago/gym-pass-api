using GymPass.API.HttpResponses;
using GymPass.Application.CQRs.Queries.Requests;
using GymPass.Application.CQRs.Queries.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymPass.API.Controllers.CheckIns;

[ApiController]
[Route("api/check-ins/user/{userId}/history")]
[ApiExplorerSettings(GroupName = "Check-Ins")]
public class FetchUserCheckInsHistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public FetchUserCheckInsHistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FetchUserCheckInsHistoryResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseError))]
    public async Task<IActionResult> Handle([FromRoute] string userId)
    {
        FetchUserCheckInsHistoryResponse response = await _mediator.Send(new FetchUserCheckInsHistoryQuery()
        {
            UserId = userId
        });
        
        return Ok(response);
    }
}