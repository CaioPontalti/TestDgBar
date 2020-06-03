using Clearsale.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Clearsale.Domain.Models
{
    public class Product 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double ValueDiscount { get; set; }

        public Product()
        {

        }

        public Product(string name, double value, double valueDiscount)
        {
            Name = name;
            Value = value;
            ValueDiscount = valueDiscount;
        }
    }
}
