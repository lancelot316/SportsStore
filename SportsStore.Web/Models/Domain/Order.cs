﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Web.Models.Domain
{
    public class Order
    {
        
        public int OrderID { get; set; }
        
        public virtual List<OrderLine> Lines { get; set; } = new List<OrderLine>();
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the first address line")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter a state name")]
        public string State { get; set; }
        public string Zip { get; set; }
        [Required(ErrorMessage = "Please enter a country name")]
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
    }

    public class OrderLine
    {
        public int OrderLineID { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}