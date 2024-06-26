using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace itemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static List<Item> Items = new List<Item>
    {
        new Item { Name = "Item1", Code = "001", Brand = "BrandA", UnitPrice = 10.0m },
        new Item { Name = "Item2", Code = "002", Brand = "BrandB", UnitPrice = 20.0m }
    };

        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            return Items;
        }

        [HttpGet("{code}")]
        public ActionResult<Item> GetItem(string code)
        {
            var item = Items.FirstOrDefault(i => i.Code == code);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public ActionResult<Item> CreateItem(Item item)
        {
            Items.Add(item);
            return CreatedAtAction(nameof(GetItem), new { code = item.Code }, item);
        }

        [HttpPut("{code}")]
        public IActionResult UpdateItem(string code, Item item)
        {
            var existingItem = Items.FirstOrDefault(i => i.Code == code);
            if (existingItem == null)
            {
                return NotFound();
            }
            existingItem.Name = item.Name;
            existingItem.Brand = item.Brand;
            existingItem.UnitPrice = item.UnitPrice;
            return NoContent();
        }

        [HttpDelete("{code}")]
        public IActionResult DeleteItem(string code)
        {
            var item = Items.FirstOrDefault(i => i.Code == code);
            if (item == null)
            {
                return NotFound();
            }
            Items.Remove(item);
            return NoContent();
        }
    }
}
