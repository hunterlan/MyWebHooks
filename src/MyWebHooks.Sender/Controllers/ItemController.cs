using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyWebHooks.Sender.Models;
using MyWebHooks.Sender.Services;

namespace MyWebHooks.Sender.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Item>> Get()
        {
            return Ok(_itemService.GetItems());    
        }

        [HttpPost(Name = "PostItem")]
        public IActionResult Post([FromBody] Item item)
        {
            try
            {
                item.Id = Guid.CreateVersion7().ToString();
                _itemService.AddItem(item);
                // TODO: Generate an event for subs
                return CreatedAtAction("Post", item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
