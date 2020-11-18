using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntityFramework_MasterDetailExample.Models;

namespace EntityFramework_MasterDetailExample.Controllers
{
    public class OrdersController : Controller
    {
        private SupplierStoreEntities db = new SupplierStoreEntities();

        // GET: Orders
        public async Task<ActionResult> Index() 
        {
         
            //use for alert BOX MESSAGE ON VIEW
            if (TempData["Message"] == null)
            {
                ViewBag.Message = "Welcome Admin :)";
            }
            else
            {
                ViewBag.Message = TempData["Message"] as string;
            }

            var orders = db.Orders.Include(o => o.OrderStatu);

            return View(await orders.ToListAsync());        
        }


        // GET: Orders/Create

        public async Task<ActionResult> Create()
        {
            Order newOrder = new Order();
            //get todays date and time 
            newOrder.DateTime = DateTime.Now;
            newOrder.StatusID = 1;
            db.Orders.Add(newOrder);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Confirm Order 

        public static List<bindedlist> currentOrders = new List<bindedlist>();

        public async Task<ActionResult> ConfirmOrder(int id)
        {
            ViewBag.OrderID = id;

            var orderLines = currentOrders.ToList();

            //Craziblameiaiany code

            foreach (bindedlist checkobject in currentOrders)
            {
                /*Find Supplier in Db*/
               var SuplierIndb =  db.Suppliers.Where(x => x.SupplierID == checkobject.SupplierID).FirstOrDefault();
                /*Change binded list name to be db object name */
                checkobject.SupplierName = SuplierIndb.Name;

                /*Find Supplier in Db*/
                var ProductIndb = db.Products.Where(x => x.ProductID == checkobject.ProductID).FirstOrDefault();
                /*Change binded list name to be db object name */
                checkobject.ProductName = ProductIndb.Name;
                checkobject.ProductPrice = ProductIndb.Price;
            }

            //3 select list objects 

            //product
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name"); //tweak to work 
            //supplier
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "Name");
            //

            double Total = 0;
            foreach(bindedlist checkobject in currentOrders)
            {
              var TotalForrow = currentOrders.Sum(x => x.ProductPrice * x.Quantity);
              Total =+ TotalForrow;
            }

            ViewBag.TotalPrice = Total;

            ViewBag.TotalQuantity = currentOrders.Sum(x => x.Quantity);


            //use for alert BOX MESSAGE ON VIEW
            if (TempData["Message"] == null)
            {
                ViewBag.Message = "Welcome Back ";
            }
            else
            {
                ViewBag.Message = TempData["Message"] as string;
            }

            return View(orderLines);
        }

       
        //InsertRecord

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertRecord([Bind(Include = "OrderID,SupplierID,ProductID,Quantity,OrderLineID")] bindedlist orderLine)
        {
            if (ModelState.IsValid)
            {
                currentOrders.Add(orderLine);
                TempData["Message"] = "Successfully Added";
                return RedirectToAction("ConfirmOrder", new { id = orderLine.OrderID });
            }
            else
            {
                TempData["Message"] = "Failed to Add Order Please check Input fields."; //add functionality
                return RedirectToAction("ConfirmOrder");
            }
        }


        //Send Order
        public async Task<ActionResult> SendOrder(int id )
        {
            List<OrderLine> AddThesetoDB = new List<OrderLine>();

            foreach(bindedlist checkobject in currentOrders)
            {
                OrderLine newObj = new OrderLine();
                newObj.OrderID = id;
                newObj.SupplierID = checkobject.SupplierID;
                newObj.ProductID = checkobject.ProductID;
                newObj.Quantity = checkobject.Quantity;
                AddThesetoDB.Add(newObj);
            }
            db.OrderLines.AddRange(AddThesetoDB);
            await db.SaveChangesAsync();
            //make sure that the orderstaus changes to sent 


            var findOrder = db.Orders.Where(x => x.OrderID == id).FirstOrDefault();
            findOrder.StatusID = 2;
            db.Entry(findOrder).State = EntityState.Modified;
            await db.SaveChangesAsync();
            //Done 

            currentOrders.Clear();

            TempData["Message"] = "Success Order has been Sent"; //add functionality
            return RedirectToAction("Index");   
        }

        //View Order
        public async Task<ActionResult> ViewOrder(int id)
        {
            var orderLines = db.OrderLines.Include(o => o.Order).Include(o => o.Product).Include(o => o.Supplier).Where(x => x.OrderID == id);
            ViewBag.OrderTotal = orderLines.Sum(x => x.Product.Price);
            return View(await orderLines.ToListAsync());
        }


        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
            TempData["Message"] = "Successfully Removed Order";
            return RedirectToAction("Index");
        }

   
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
