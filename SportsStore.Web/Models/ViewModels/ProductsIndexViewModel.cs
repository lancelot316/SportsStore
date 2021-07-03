using SportsStore.Web.Models.Domain;
using System.Collections.Generic;

namespace SportsStore.Web.Models.ViewModels
{
    public class ProductsIndexViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}