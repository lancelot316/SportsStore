using Microsoft.AspNetCore.Mvc;
using SportsStore.Web.Models.Domain;

namespace SportsStore.Web.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            repository = repoService;
            cart = cartService;
        }
        public ViewResult Checkout() => View(new Order());
        
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                foreach (var line in cart.Lines)
                {
                    order.Lines.Add(
                        new OrderLine
                        {
                            Order = order,
                            Product = line.Product,
                            Quantity = line.Quantity
                        });
                }

                repository.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/Completed", new { orderId = order.OrderID });
            }
            else
            {
                return View();
            }
        }
    }
}
