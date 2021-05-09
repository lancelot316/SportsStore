using System.Collections.Generic;

namespace SportsStore.Models.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        void DeleteProduct(Product product);
        void SaveProduct(Product product);
    }
}