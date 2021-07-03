using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Web.Infrastructure;
using SportsStore.Web.Models.Domain;

namespace SportsStore.Web.Pages
{
    public class CartModel : PageModel
    {
        private IProductRepository repository;
        
        public CartModel(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            Cart = cartService;
        }
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }
        public IActionResult OnPost(long productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            Cart.AddItem(product, 1);
            
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(long productId, string returnUrl)
        {
            var p = Cart.Lines.First(cl => cl.Product.ProductID == productId).Product;
            
            Cart.RemoveLine(p);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
