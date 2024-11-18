using GymPass.API.HttpResponses;
using GymPass.Application.CQRs.Commands.Requests;
using GymPass.Application.CQRs.Commands.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymPass.API.Controllers.CheckIns;

[ApiController]
[Route("api/v1/check-ins/{checkInId}/validate")]
[ApiExplorerSettings(GroupName = "Check-Ins")]
public class ValidateCheckInController : ControllerBase
{
    private readonly IMediator _mediator;

    public ValidateCheckInController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ValidateCheckInResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseError))]
    public async Task<IActionResult> Handle([FromRoute] string checkInId)
    {
        ValidateCheckInCommand commnad = new()
        {
            CheckInId = checkInId
        };
        
        ValidateCheckInResponse result = await _mediator.Send(commnad);

        return Ok(result);
    }
}