using GymPass.Application.CQRs.Queries.Requests;
using GymPass.Application.CQRs.Queries.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymPass.API.Controllers.Gyms;

[ApiController]
[Route("api/gyms")]
[ApiExplorerSettings(GroupName = "Gyms")]
public class SearchGymController : ControllerBase
{
    private readonly IMediator _mediator;

    public SearchGymController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchGymsQueryResponse))]
    public async Task<IActionResult> Handle([FromQuery] string query, [FromQuery] int page)
    {
        SeachGymsQuery gymsQuery = new()
        {
            Query = query,
            Page = page
        };
        
        SearchGymsQueryResponse response = await _mediator.Send(gymsQuery);

        return Ok(response);
    }
}