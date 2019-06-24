using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRService.Hubs;
using SignalRService.Models;

namespace SignalRService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> _products = new List<Product>();
        private IHubContext<ProductsHub> _hub;
        public ProductsController(IHubContext<ProductsHub> hub)
        {
            _hub = hub;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return _products;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody] Product value)
        {
            _products.Add(value);
            await _hub.Clients.All.SendAsync("Add", value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product value)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            product = value;
        }
        [HttpPut("changeQuantity")]
        public async void ChangeQuantity(int id, int quantity)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            product.Quantity = quantity;
            await _hub.Clients.All.SendAsync("ChangeQuantity", id, quantity);
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            _products.Remove(_products.FirstOrDefault(p => p.Id == id));
            await _hub.Clients.All.SendAsync("Delete", id);
        }
    }
}
