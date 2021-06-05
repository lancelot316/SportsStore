using SportsStore.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportsStore.Web.Models.ViewModels;
using SportsStore.Web.Infratructure;

namespace SportsStore.Web.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository productRepository;
        private IOrderRepository orderRepository;

        
        public CartController(IProductRepository productRepo, IOrderRepository orderRepo)
        {
            productRepository = productRepo;
            orderRepository = orderRepo;
        }



        public ViewResult Index(string returnUrl)
        {
            var model = new CartIndexViewModel
            {
                ReturnUrl = returnUrl,
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart()
            };
            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = productRepository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            var cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
                        
            HttpContext.Session.SetJson("cart", cart);

            return RedirectToAction("Index", new { returnUrl });
        }

        //public RedirectToRouteResult RemoveFromCart(Cart cart, int productId,
        //        string returnUrl)
        //{
        //    Product product = productRepository.Products
        //        .FirstOrDefault(p => p.ProductID == productId);

        //    if (product != null)
        //    {
        //        cart.RemoveLine(product);
        //    }
        //    return RedirectToAction("Index", new { returnUrl });
        //}

        //public PartialViewResult Summary(Cart cart)
        //{
        //    return PartialView(cart);
        //}

        //public ViewResult Checkout()
        //{
        //    return View(new Order());
        //}

        //[HttpPost]
        //public ViewResult Checkout(Cart cart, Order order)
        //{
        //    if (cart.Lines.Count() == 0)
        //    {
        //        ModelState.AddModelError("", "Sorry, your cart is empty!");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        foreach(var line in cart.Lines)
        //        {
        //            order.OrderLines.Add(
        //                new OrderLine { 
        //                    Order = order, 
        //                    Product = line.Product, 
        //                    Quantity = line.Quantity 
        //                });
        //        }
        //        orderRepository.SaveOrder(order);
        //        cart.Clear();
        //        return View("Completed");
        //    }
        //    else
        //    {
        //        return View(order);
        //    }
        //}
    }
}