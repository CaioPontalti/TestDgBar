using Clearsale.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clearsale.Domain.Models
{
    public class Order : Entity 
    {
        
        public Order(int orderNumber, int productId, double value, bool hasDiscount)
        {
            OrderNumber = orderNumber;
            ProductId = productId;
            Value = value;
            HasDiscount = hasDiscount;
        }

        public int OrderNumber { get; set; }
        public int ProductId { get; set; }
        public double Value { get; set; }
        public bool HasDiscount { get; set; }
    }
}
