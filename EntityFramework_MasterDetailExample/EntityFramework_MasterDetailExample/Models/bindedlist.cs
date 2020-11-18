using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFramework_MasterDetailExample.Models
{
    public class bindedlist
    {

        public int OrderLineID { get; set; }
        public int OrderID { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }


    }
}