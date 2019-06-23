using Microsoft.AspNetCore.SignalR;
using SignalRService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRService.Hubs
{
    public class ProductsHub : Hub<IProductClient>
    {
        public async Task Add(Product product) => await Clients.All.Add(product);
        public async Task Delete(int id) => await Clients.All.Delete(id);
    }
}
