﻿using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Application.Subscriptions.Commands.DeleteSubscription;
using GymManagement.Application.Subscriptions.Queries.GetSubscription;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISender _mediator;

    public SubscriptionsController(ISender mediator)
    {
        _mediator = mediator;
    }

    // Shall translate API message to domain message
    [HttpPost]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
    {
        if(!Domain.Subscriptions.SubscriptionType.TryFromName(request.SubscriptionType.ToString(), out var subscriptionType))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid subscription type");
        }

        var command = new CreateSubscriptionCommand(
            subscriptionType, 
            request.AdminId);

        var createSubscriptionResult = await _mediator.Send(command);

        return createSubscriptionResult.MatchFirst(
            subscription => Ok(new SubscriptionResponse(subscription.Id, request.SubscriptionType)),
            error => Problem()
        );
    }

    [HttpGet("{subscriptionId:guid}")]
    public async Task<IActionResult> GetSubscription(Guid subscriptionId)
    {
        var query = new GetSubscriptionQuery(subscriptionId);

        var getSubscriptionResult = await _mediator.Send(query);

        return getSubscriptionResult.MatchFirst(
            subscription => Ok(new SubscriptionResponse(
                subscription.Id, 
                Enum.Parse<SubscriptionType>(subscription.SubscriptionType.Name))),
            error => Problem());
    }

    [HttpDelete("{subscriptionId:guid}")]
    public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
    {
        var command = new DeleteSubscriptionCommand(subscriptionId);

        var createSubscriptionResult = await _mediator.Send(command);

        return createSubscriptionResult.Match<IActionResult>(
            _ => NoContent(),
            _ => Problem());
    }
}
