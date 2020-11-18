using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReportingExample.Models;


namespace ReportingExample.Controllers
{
    public class HomeController : Controller
    {
        private ReportStoreEntities db = new ReportStoreEntities();

        public ActionResult Index(int? productID)
        {
            //Get List of Names to load to view
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");

            //check if productID is null try find the users
            if (productID != null)
            {
                ViewBag.SelectedID = productID;

                var InvoiceLinesFound = db.InvoiceLines.Include(c => c.Invoice).Include( c => c.Product).Where(x => x.ProductID == productID).ToList();
                return View(InvoiceLinesFound);
            }
            else
            {               
                return View();
            }
        }

        public JsonResult GetChartData(int quantity)
        {
            // make list to hold things for chart
            List<ProductHolder> chartlist = new List<ProductHolder>();

            //Group Objects
            var Products = (from invoiceline
                            in db.InvoiceLines
                            group invoiceline 
                            by invoiceline.ProductID 
                            into groupedobjects
                            orderby groupedobjects.Key
                            select new ProductHolder
                            {
                                ProductID = groupedobjects.Key,
                                ProductQuantity = (int)groupedobjects.Sum(q => q.Quantity)
                            }).ToList(); ;
            //loop through each object and add name to complete model

            foreach (var line in Products)
            {
                var findProductName = db.Products.Where(x => x.ProductID == line.ProductID).FirstOrDefault();

                line.ProductName = findProductName.ProductName;

                if (line.ProductQuantity >= quantity)
                {
                    chartlist.Add(line);
                }
            }

            //-----------------------------------------------------------------------\\
            var Prod = db.InvoiceLines.GroupBy(x => x.ProductID).OrderBy(x => x.Key).ToList();
            foreach (var line in Prod)
            {
                //format it into a ProductHolder object 
                ProductHolder newProd = new ProductHolder();
                newProd.ProductID = line.Key;
                newProd.ProductQuantity = (int)line.Sum(q => q.Quantity);
                var findProductName = db.Products.Where(x => x.ProductID == newProd.ProductID).FirstOrDefault();
                newProd.ProductName = findProductName.ProductName;
                if (newProd.ProductQuantity >= quantity)
                {
                    chartlist.Add(newProd);
                }        
            }

            // send data to back
    
            return new JsonResult { Data = chartlist,  JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}