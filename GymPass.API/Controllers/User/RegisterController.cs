using GymPass.API.HttpResponses;
using GymPass.Application.CQRs.Commands.Requests;
using GymPass.Application.CQRs.Commands.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymPass.API.Controllers.User;

[ApiController]
[Route("api/v1/register")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Users")]
public class RegisterController : ControllerBase
{
    private readonly IMediator _mediator;

    public RegisterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(201, Type = typeof(RegisterResponse))]
    [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(409, Type = typeof(ResponseError))]
    public async Task<IActionResult> Handle([FromBody] RegisterCommand body)
    {
        RegisterResponse response = await _mediator.Send(body);
        
        return Created("", response);
    }
}