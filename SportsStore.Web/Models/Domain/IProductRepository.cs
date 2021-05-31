using System.Collections.Generic;

namespace SportsStore.Web.Models.Domain
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        //void DeleteProduct(Product product);
        //Product DeleteProduct(int productId);
        //void SaveProduct(Product product);
    }
}