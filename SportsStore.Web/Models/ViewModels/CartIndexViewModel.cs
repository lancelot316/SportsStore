using SportsStore.Web.Models.Domain;

namespace SportsStore.Web.Models.ViewModels
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}