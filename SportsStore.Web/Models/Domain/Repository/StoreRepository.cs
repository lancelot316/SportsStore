using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Web.Models.Domain.Repository
{
    public class StoreRepository : IProductRepository, IOrderRepository
    {
        private EFDbContext context;

        public StoreRepository(EFDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        //public void SaveProduct(Product product)
        //{
        //    if (product.ProductID == 0)
        //    {
        //        product = context.Products.Add(product);
        //    }
        //    else
        //    {
        //        Product dbProduct = context.Products.Find(product.ProductID);
        //        if (dbProduct != null)
        //        {
        //            dbProduct.Name = product.Name;
        //            dbProduct.Description = product.Description;
        //            dbProduct.Price = product.Price;
        //            dbProduct.Category = product.Category;
        //            dbProduct.ImageData = product.ImageData;
        //            dbProduct.ImageMimeType = product.ImageMimeType;
        //        }
        //    }
        //    context.SaveChanges();
        //}

        //public void DeleteProduct(Product product)
        //{
        //    IEnumerable<Order> orders = context.Orders
        //        .Include(o => o.OrderLines.Select(ol => ol.Product))
        //        .Where(o => o.OrderLines.Count(ol => ol.Product
        //            .ProductID == product.ProductID) > 0).ToArray();

        //    foreach (Order order in orders)
        //    {
        //        context.Orders.Remove(order);
        //    }
        //    context.Products.Remove(product);
        //    context.SaveChanges();
        //}

        //public Product DeleteProduct(int productID)
        //{
        //    Product dbEntry = context.Products.Find(productID);
        //    if (dbEntry != null)
        //    {
        //        context.Products.Remove(dbEntry);
        //        context.SaveChanges();
        //    }
        //    return dbEntry;
        //}

        public IQueryable<Order> Orders => context.Orders
                            .Include(o => o.Lines)
                            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));

            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }

            //if (order.OrderID == 0)
            //{
            //    context.Orders.Add(order);

            //    foreach (OrderLine line in order.OrderLines)
            //    {
            //        context.Entry(line.Product).State = EntityState.Modified;
            //    }
            //}
            //else
            //{
            //    Order dbOrder = context.Orders.Find(order.OrderID);
            //    if (dbOrder != null)
            //    {
            //        dbOrder.Name = order.Name;
            //        dbOrder.Line1 = order.Line1;
            //        dbOrder.Line2 = order.Line2;
            //        dbOrder.Line3 = order.Line3;
            //        dbOrder.City = order.City;
            //        dbOrder.State = order.State;
            //        dbOrder.Zip = order.Zip;
            //        dbOrder.Country = order.Country;
            //        dbOrder.GiftWrap = order.GiftWrap;
            //    }
            //}
            context.SaveChanges();
        }
    }
}