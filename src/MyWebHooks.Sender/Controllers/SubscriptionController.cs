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

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
        
        [HttpPost]
        public IActionResult CreateSubscription([FromBody] WebhookSubscriptionDto subscription)
        {
            try
            {
                var generatedId = _subscriptionService.Create(subscription);
                return Created();
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
