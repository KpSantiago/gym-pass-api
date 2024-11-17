using GymPass.API.HttpResponses;
using GymPass.Application.CQRs.Commands.Requests;
using GymPass.Application.CQRs.Commands.Responses;
using GymPass.Domain.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymPass.API.Controllers.User;

[ApiController]
[Route("api/v1/session")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Users")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccessToken))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseError))]
    public async Task<IActionResult> Handle([FromBody] AuthCommand body)
    {
        AuthResponse response = await _mediator.Send(body);

        return Ok(new {response.AccessToken});
    }
}