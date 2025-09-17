using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyWebHooks.Sender.Models;
using MyWebHooks.Sender.Services;
using MyWebHooks.Sender.Services.Events;
using MyWebHooks.Sender.Services.Items;
using MyWebHooks.Sender.Services.Subscriptions;

namespace MyWebHooks.Sender.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItemService _itemService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IEventService _eventService;

        public ItemController(IItemService itemService, ISubscriptionService subscriptionService, 
            IEventService eventService, ILogger<ItemController> logger)
        {
            _itemService = itemService;
            _subscriptionService = subscriptionService;
            _eventService = eventService;
            _logger = logger;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Item>> Get()
        {
            return Ok(_itemService.GetItems());    
        }

        [HttpPost(Name = "PostItem")]
        public async Task<IActionResult> Post([FromBody] Item item)
        {
            try
            {
                item.Id = Guid.CreateVersion7().ToString();
                _itemService.AddItem(item);
                _logger.LogInformation("Item {ItemId} has been added", item.Id);
                //TODO: Wrap with try catch
                //TODO: Think about how to launch it after returning a status code to a client
                var serializedItem = JsonSerializer.Serialize(item);
                var subscriptions = _subscriptionService.GetAllByEventType(SubEventType.ItemAdded);
                var eventId = _eventService.Create(serializedItem, SubEventType.ItemAdded);
                var sendersList = await _eventService.SendAsync(eventId, subscriptions);

                return CreatedAtAction("Post", item);
            }
            catch (ArgumentException aex)
            {
                _logger.LogWarning("Item {ItemId} has not been added. Reason: {message}", item.Id, aex.Message);
                return BadRequest(aex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}
