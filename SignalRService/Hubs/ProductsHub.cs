﻿using Microsoft.AspNetCore.SignalR;
using SignalRService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRService.Hubs
{
    /// <summary>
    /// Used for providing realtime updates for the ProductsController.
    /// </summary>
    public class ProductsHub : Hub<IProductsClient>
    {
        public async Task Add(Product product) => await Clients.All.Add(product);
        public async Task Delete(int id) => await Clients.All.Delete(id);
        public async Task ChangeQuantity(int changeQuantity) => await Clients.All.ChangeQuantity(changeQuantity);
    }
}
