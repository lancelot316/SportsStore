using SportsStore.Domain;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.Models.Repository;
using SportsStore.WebUI.Pages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace SportsStore.WebUI.Pages
{
    public partial class Listing : System.Web.UI.Page
    {
        private int pageSize = 4;
        private Repository repo = new Repository();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                int selectedProductId;
                if (int.TryParse(Request.Form["add"], out selectedProductId))
                {
                    Product selectedProduct = repo.Products
                        .Where(p => p.ProductID == selectedProductId).FirstOrDefault();
                    if (selectedProduct != null)
                    {
                        SessionHelper.GetCart(Session).AddItem(selectedProduct, 1);
                        SessionHelper.Set(Session, SessionKey.RETURN_URL,
                            Request.RawUrl);

                        Response.Redirect(RouteTable.Routes
                            .GetVirtualPath(null, "cart", null).VirtualPath);
                    }
                }
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            return FilterProducts()
                .OrderBy(p => p.ProductID)
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize);
        }

        public IEnumerable<string> GetPagerLinkHtml()
        {
            List<string> links = new List<string>(MaxPage);

            for (int i = 1; i <= MaxPage; i++)
            {
                string path = RouteTable.Routes.GetVirtualPath(null, null, new RouteValueDictionary() { { "page", i } }).VirtualPath;

                links.Add(string.Format("<a href='{0}' {1}>{2}</a>",
                    path, i == CurrentPage ? "class='selected'" : "", i));
            }

            return links;
        }

        private int CurrentPage
        {
            get
            {
                int page;
                page = GetPageFromRequest();
                return page > MaxPage ? MaxPage : page;
            }
        }

        private int MaxPage
        {
            get
            {
                return (int)Math.Ceiling((decimal)FilterProducts().Count() / pageSize);
            }
        }

        private IEnumerable<Product> FilterProducts()
        {
            IEnumerable<Product> products = repo.Products;
            string currentCategory = (string)RouteData.Values["category"] ??
                Request.QueryString["category"];
            return currentCategory == null ? products
                : products.Where(p => p.Category == currentCategory);
        }

        private int GetPageFromRequest()
        {
            int page;
            string reqValue = (string)RouteData.Values["page"] ?? Request.QueryString["page"];

            return reqValue != null && int.TryParse(reqValue, out page) ? page : 1;
        }
    }
}