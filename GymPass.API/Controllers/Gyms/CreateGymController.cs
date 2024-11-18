using System.Security.Claims;
using GymPass.API.Middlewares;
using GymPass.Application.CQRs.Commands.Requests;
using GymPass.Application.CQRs.Commands.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymPass.API.Controllers.Gyms;

[ApiController]
[Route("api/v1/gyms")]
[ApiExplorerSettings(GroupName = "Gyms")]
public class CreateGymController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateGymController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateGymResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> Handle([FromBody] CreateGymCommand body)
    {
        IEnumerable<Claim> userClaims = User.Claims;
        
        RolesMiddleware.VerifyRole("Admin", userClaims);
        
        CreateGymResponse response = await _mediator.Send(body);

        return Created("", response);
    }
}