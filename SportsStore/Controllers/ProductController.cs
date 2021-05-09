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
        private Repository _repo;

        public ProductController(Repository repo)
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