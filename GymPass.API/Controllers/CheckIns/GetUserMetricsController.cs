using GymPass.Application.CQRs.Queries.Requests;
using GymPass.Application.CQRs.Queries.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymPass.API.Controllers.CheckIns;

[ApiController]
[Route("api/v1/check-ins/user/{userId}/metrics")]
[ApiExplorerSettings(GroupName = "Check-Ins")]
public class GetUserMetricsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetUserMetricsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserMetricsResponse))]
    public async Task<IActionResult> Handle(string userId)
    {
        GetUserMetricsResponse response = await _mediator.Send(new GetUserMetricsQuery()
        {
            UserId = userId
        });
        
        return Ok(response);
    }
}