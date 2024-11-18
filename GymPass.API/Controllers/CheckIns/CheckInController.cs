using GymPass.API.HttpResponses;
using GymPass.Application.CQRs.Commands.Requests;
using GymPass.Application.CQRs.Commands.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymPass.API.Controllers.CheckIns;

[ApiController]
[Route("api/v1/check-ins")]
[ApiExplorerSettings(GroupName = "Check-Ins")]
public class CheckInController : ControllerBase
{
    private readonly IMediator _mediator;

    public CheckInController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CheckInResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseError))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseError))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ResponseError))]
    public async Task<IActionResult> Handle([FromBody] CheckInCommand body)
    {
        CheckInResponse response = await _mediator.Send(body);

        return Created("", response);
    }
}