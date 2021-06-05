using System.Linq;

namespace SportsStore.Web.Models.Domain
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }

        void SaveOrder(Order order);
    }
}
