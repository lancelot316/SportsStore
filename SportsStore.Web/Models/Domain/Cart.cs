﻿using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Web.Models.Domain
{
    public class Cart
    {
        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = Lines
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product)
        {
            Lines.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        public decimal ComputeTotalValue()
        {
            return Lines.Sum(e => e.Product.Price * e.Quantity);

        }
        public virtual void Clear()
        {
            Lines.Clear();
        }

        public List<CartLine> Lines { get; set; } = new List<CartLine>();

    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}