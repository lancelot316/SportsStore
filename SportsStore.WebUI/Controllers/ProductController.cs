using SportsStore.Domain;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repo;

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }


        // GET: Product
        public ActionResult List()
        {
            IEnumerable<Product> products = _repo.Products;

            return View(products);
        }
    }
}