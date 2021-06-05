using System.Collections.Generic;

namespace SportsStore.Web.Models.Domain
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; }

        void SaveOrder(Order order);
    }
}
