using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpressCheckoutASP.net.Models
{
    public class Category
    {   
        private List<Product> products;

        public Category()
        {
            products = new List<Product>();
        }

        public int Id { get; set; }
        public String Name { get; set; }
        public List<Product> Products
        {
            get { return products; }
        }
    }
}
