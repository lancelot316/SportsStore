using SportsStore.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportsStore.Web.Models.ViewModels;
using SportsStore.Web.Infrastructure;

namespace SportsStore.Web.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository productRepository;
        private IOrderRepository orderRepository;
        private Cart cart;


        public CartController(IProductRepository productRepo, IOrderRepository orderRepo, Cart cart)
        {
            productRepository = productRepo;
            orderRepository = orderRepo;
            this.cart = cart;
        }



        public ViewResult Index(string returnUrl)
        {
            var model = new CartIndexViewModel
            {
                ReturnUrl = returnUrl ?? "/",
                Cart = cart
            };
            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = productRepository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productID, string returnUrl)
        {
            Product product = productRepository.Products
                .FirstOrDefault(p => p.ProductID == productID);

            if (product != null)
            {
                cart.RemoveLine(product);
            }

            return Redirect(returnUrl);
        }

        public ViewResult Checkout()
        {
            return View(new Order());
            //if (cart.Lines.Count() == 0)
            //{
            //    ModelState.AddModelError("", "Sorry, your cart is empty!");
            //}

            //if (ModelState.IsValid)
            //{
            //    foreach (var line in cart.Lines)
            //    {
            //        order.OrderLines.Add(
            //            new OrderLine
            //            {
            //                Order = order,
            //                Product = line.Product,
            //                Quantity = line.Quantity
            //            });
            //    }
            //    orderRepository.SaveOrder(order);
            //    cart.Clear();
            //    return View("Completed");
            //}
            //else
            //{
            //    return View(order);
            //}
        }
    }
}