using System.Security.Claims;
using GymPass.API.HttpResponses;
using GymPass.Application.CQRs.Queries.Requests;
using GymPass.Application.CQRs.Queries.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymPass.API.Controllers.User;

[ApiController]
[Route("api/v1/user/profile")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Users")]
public class GetUserProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetUserProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize] 
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserProfileResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseError))]
    public async Task<IActionResult> Handle()
    { 
        Claim? sub = User.Claims.FirstOrDefault(c => c.Type == "sub");
        
        if (sub == null)
            return Unauthorized();
        
        GetUserProfileResponse response = await _mediator.Send(new GetUserProfileQuery {UserId = sub.Value});
        
        return Ok(response);
    }
}