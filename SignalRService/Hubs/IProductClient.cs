using SignalRService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRService.Hubs
{
    public interface IProductClient
    {
        Task Add(Product product);
        Task Delete(int id); 
    }
}
