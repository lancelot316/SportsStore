using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Web.Models.Domain;

namespace SportsStore.Web.Pages.Orders
{
    public class CheckoutModel : PageModel
    {
        [BindProperty]
        public Order Order { get; set; }
        public CheckoutModel(IOrderRepository repository, Cart cart)
        {
            this.repository = repository;
            this.cart = cart;
        }

        public void OnGet()
        {
            Order = new Order();
        }

        public IActionResult OnPost(Order order)
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
                
                return RedirectToPage("/Orders/Completed", new { orderId = order.OrderID });
            }

            return Page();
        }

        private readonly IOrderRepository repository;
        private readonly Cart cart;
    }
}
