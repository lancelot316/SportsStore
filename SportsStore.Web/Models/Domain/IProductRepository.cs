using System.Linq;

namespace SportsStore.Web.Models.Domain
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        //void DeleteProduct(Product product);
        Product DeleteProduct(int productId);
        void SaveProduct(Product product);
    }
}