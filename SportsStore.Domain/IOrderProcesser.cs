using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, Order order);
    }
}
