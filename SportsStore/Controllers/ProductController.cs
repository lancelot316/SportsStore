using SportsStore.Models;
using SportsStore.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.Controllers
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