using GymPass.Application.CQRs.Queries.Requests;
using GymPass.Application.CQRs.Queries.Responses;
using GymPass.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymPass.API.Controllers.Gyms;

[ApiController]
[Route("api/v1/gyms/nearby")]
[ApiExplorerSettings(GroupName = "Gyms")]
public class FetchNearbyGymsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FetchNearbyGymsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FetchNearbyGymsResponse))]
    public async Task<IActionResult> Handle([FromQuery] double latitude, [FromQuery] double longitude)
    {
        FetchNearbyGymsResponse response = await _mediator.Send(new FetchNearbyGymsQuery()
        {
            Cordinate = new Cordinate(latitude, longitude)
        });
        
        return Ok(response);
    }
    
}