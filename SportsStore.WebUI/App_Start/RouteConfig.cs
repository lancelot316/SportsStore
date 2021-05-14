using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
                "",
                new
                {
                    controller = "Product",
                    action = "List",
                    category = (string)null,
                    page = 1
                }
            );

            routes.MapRoute(null,
                "Page{page}",
                new { controller = "Product", action = "List", category = (string)null },
                new { page = @"\d+" }
            );

            routes.MapRoute(null,
                "{category}",
                new { controller = "Product", action = "List", page = 1 }
            );

            routes.MapRoute(null,
                "{category}/Page{page}",
                new { controller = "Product", action = "List" },
                new { page = @"\d+" }
            );

            routes.MapRoute(null, "{controller}/{action}");

            //routes.MapPageRoute(null, "list/{category}/{page}", "~/Pages/Listing.aspx");
            //routes.MapPageRoute(null, "list/{page}", "~/Pages/Listing.aspx");
            //routes.MapPageRoute(null, "", "~/Pages/Listing.aspx");
            //routes.MapPageRoute(null, "list", "~/Pages/Listing.aspx");

            //routes.MapPageRoute("cart", "cart", "~/Pages/CartView.aspx");
            //routes.MapPageRoute("checkout", "checkout", "~/Pages/Checkout.aspx");

            //routes.MapPageRoute("admin_orders", "admin/orders", "~/Pages/Admin/Orders.aspx");
            //routes.MapPageRoute("admin_products", "admin/products", "~/Pages/Admin/Products.aspx");
            
        }
    }
}