using SignalRService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRService.Hubs
{
    /// <summary>
    /// Defines the events associated with the Hub.
    /// </summary>
    public interface IProductsClient
    {
        Task Add(Product product);
        Task Delete(int id);
        Task ChangeQuantity(int quantity);
    }
}
