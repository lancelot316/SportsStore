using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Web.Models.Domain;
using System.IO;
using System.Linq;

namespace SportsStore.Web.Controllers
{

    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            ViewBag.Message = TempData["message"];

            return View(repository.Products);
        }

        public IActionResult Details(int productId)
        {
            var model = repository.Products
                .Where(x => x.ProductID == productId)
                .FirstOrDefault();

            return View(model);
        }

        public ViewResult Edit(int productId)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, IFormFile image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.Length];
                    using (var ms = new MemoryStream())
                    {
                        image.CopyTo(ms);
                        product.ImageData = ms.ToArray();
                    }
                }

                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            
            return View(product);
        }

        public ViewResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if(ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been created";
                return RedirectToAction("Index");
            }

            return View(new Product());
        }

        public ActionResult Delete(int productId)
        {
            var product = repository.DeleteProduct(productId);

            if(product != null)
            {
                TempData["message"] = $"{product.Name} has been deleted";
            }
           
            return RedirectToAction("Index");
        }
    }
}