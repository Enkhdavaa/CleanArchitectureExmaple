using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Contracts.Gyms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("subscriptions/{subscriptionId:guid}/gyms")]
public class GymController : ControllerBase
{
    private readonly ISender _mediator;

    public GymController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGym(CreateGymRequest request, Guid subscriptionId)
    {
        var command = new CreateGymCommand(request.Name, subscriptionId);
        var createGymResult = _mediator.Send(command);
        
        return Ok();
    }
}
