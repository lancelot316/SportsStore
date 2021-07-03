using Microsoft.AspNetCore.Mvc;
using SportsStore.Web.Models.Domain;
using SportsStore.Web.Models.ViewModels;
using System.Linq;

namespace SportsStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository _repo;
        public int PageSize = 4;

        public HomeController(IProductRepository repo)
        {
            _repo = repo;
        }


        public ViewResult Index(string category, int page = 1)
        {
            ProductsIndexViewModel model = new ProductsIndexViewModel
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
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int productId)
        {
            Product prod = _repo.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
