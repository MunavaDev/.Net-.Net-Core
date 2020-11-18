using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutodev_Assignment4_OOPMVC.Models;

namespace Tutodev_Assignment4_OOPMVC.Controllers
{
    public class CartController : Controller
    {
        //Declare list in Cart Controller to hold Cart Items from Home View
        List<ConsoleI> CartItems = new List<ConsoleI>();

        public ActionResult CartView()
        {

            var Msg = TempData["Message"] as string;

            if (Msg == null || Msg == "")
            {
                ViewBag.Message = "Welcome";
                ViewBag.Total = "0.00";

            }
            else
            {
                ViewBag.Message = Msg;
            }

            //check the count of items in incoming list
            var currentlist = TempData["CartItems"] as List<ConsoleI>;

            if(currentlist.Count() == 0)
            {
                return View();
            }
            else
            {
                //add incoming List to local List
                CartItems.AddRange(currentlist);
                //Linq query to sort and group Occourances of Items in the list 
                var console = CartItems.OrderBy(a => a.Name).GroupBy(a => a.Name, (key, items) => new CartList
                {
                    Name = key,
                    Quntity = items.Count(),
                    Price = items.Sum(item => Convert.ToInt32(item.Price))
                }).ToList();
                //get totoal using linq sum attribute 
                ViewBag.Total = CartItems.Sum(price => price.Price);
                return View(console);
            }

        }

        //Clear Basket Action is Performed 
        public ActionResult ResetShopItems()
        {
            CartItems.Clear();
            return RedirectToAction("ResetShopItems", "Home");
        }

        public ActionResult ConfirmCheckout(string Total)
        {
            ViewBag.Total = Total;
            ViewBag.Time = DateTime.Now.ToShortDateString();
            return View();
        }

        public ActionResult Checkout()
        {
            CartItems.Clear();
            return RedirectToAction("CheckoutClear", "Home");
        }

    }

}
