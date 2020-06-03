using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Clearsale.Domain.Queries
{
    public class orderResult
    {
        public orderResult()
        {
            Items = new List<orderItems>();
        }

        public List<orderItems> Items { get; set; }

        public double Total_Order { get; set; }
    }

    public class orderItems
    {
        public string Product { get; set; }
        public int Amount { get; set; }
        public double Total_Item { get; set; }
        public double Discount { get; set; }
        public double Total_Pay { get; set; }
    }
}
