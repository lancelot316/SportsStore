using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.WebUI.Models.Domain
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, Order order);
    }
}
