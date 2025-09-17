using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebHooks.Sender.DTOs;
using MyWebHooks.Sender.Services.Subscriptions;

namespace MyWebHooks.Sender.Controllers
{
    [Route("api/subscriptions")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ILogger<SubscriptionController> _logger;

        public SubscriptionController(ISubscriptionService subscriptionService,  ILogger<SubscriptionController> logger)
        {
            _subscriptionService = subscriptionService;
            _logger = logger;
        }
        
        [HttpPost]
        public IActionResult CreateSubscription([FromBody] WebhookSubscriptionDto subscription)
        {
            try
            {
                var generatedId = _subscriptionService.Create(subscription);
                _logger.LogInformation("Created subscription {id}", generatedId);
                return Created();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Attempt to subscribe again. Client: {uniqueName}:{clientIp}", subscription.UniqueName, 
                    Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP");
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't create subscription for client {uniqueName}. Reason: {message}", 
                    subscription.UniqueName, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
