using SportsStore.Domain;
using SportsStore.WebUI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repo;
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }


        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = _repo.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        _repo.Products.Count() :
                        _repo.Products.Where(e => e.Category == category).Count()
                }
            };
            return View(model);
        }
    }
}