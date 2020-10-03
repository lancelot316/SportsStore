using SportsStore.Models;
using SportsStore.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Pages
{
    public partial class Listing : System.Web.UI.Page
    {
        private int pageSize = 4;
        private Repository repo = new Repository();


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected IEnumerable<Product> GetProducts()
        {
            return FilterProducts()
                .OrderBy(p => p.ProductID)
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize);
        }

        protected int CurrentPage
        {
            get
            {
                int page;
                page = GetPageFromRequest();
                return page > MaxPage ? MaxPage : page;
            }
        }

        protected int MaxPage
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