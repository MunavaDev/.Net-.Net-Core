using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutodev_Assignment4_OOPMVC.Models;

namespace Tutodev_Assignment4_OOPMVC.Controllers
{
    public class HomeController : Controller
    {
        // Declare 3 lists to store Objects
        public static List<Playstation> playstations = new List<Playstation>();
        public static List<Xbox> xboxs = new List<Xbox>();
        public static List<Nintendo> ntendos = new List<Nintendo>();

        // On program startup add static shop data to lists 
        public ActionResult CreateIndex()
        {
            //check if store already has items --> For when user refreshes page
            if(StoreItems.Count() == 0)
            {
                //Create Diffrent concole Models

                //PS4
                playstations.Add(new Playstation(1, "Playstation 1", "Reto Ps with Gret finish", "2001_01PS1",845.37, 10,"~/Content/Images/PS/Ps1.png",new DateTime(2001, 1, 18).Date, "3 working days", "PSN120934", "5"));
                playstations.Add(new Playstation(2, "Playstation 2", "Second Gen console with 8mb Video cards", "2006_01PS2", 1000.50, 8, "~/Content/Images/PS/Ps2.png", new DateTime(2004, 1, 18).Date, "6 working days", "PSN120935", "6.5"));
                playstations.Add(new Playstation(3, "Playstation 3", "Redesigned modern console with Dualshock 4 bluetooth controllers", "2011_01PS3", 1980.00, 10,"~/Content/Images/PS/Ps3.png", new DateTime(2011, 1, 18).Date, "4 working days", "PSN120936", "8.5"));
                playstations.Add(new Playstation(4, "Playstation 4", "Latest gen console with 4k graphics cards", "2016_01PS4", 4500.00, 3,"~/Content/Images/PS/Ps4.png", new DateTime(2015, 1, 18).Date, "2 working days", "PSN120934", "9.8"));
                //add mplaystations to store list
                StoreItems.AddRange(playstations);

                //Xbox

                xboxs.Add(new Xbox(1, "xbox 360", "Intel based i2 pc inspired console", "xb_360_01", 656.89, 10, "~/Content/Images/XB/box1.png", new DateTime(2001, 1, 26).Date, "3 working days", "xb36000", "AAA"));
                xboxs.Add(new Xbox(2, "xbox 730", "Upgraded Intel chipped game centric", "xb_360_2019", 1290.00, 10, "~/Content/Images/XB/box2.png", new DateTime(2005, 1, 14).Date, "2 working days", "xb36000", "AA3"));
                xboxs.Add(new Xbox(3, "xbox oneS", "2019 white finished  console", "xb_360_2222", 1890.00, 9, "~/Content/Images/XB/box3.png", new DateTime(2017, 1, 13).Date, "1 working days", "xb36000", "AA_IO"));
                xboxs.Add(new Xbox(4, "xbox Prime", "Maxed out Prime version", "xb_360_2343", 3567.00, 4, "~/Content/Images/XB/box4.png", new DateTime(2016, 1, 12).Date, "6 working days", "xb36000", "AAB"));
                //add mplaystations to store list
                StoreItems.AddRange(xboxs);


                //Nintendo
                ntendos.Add(new Nintendo(1, "Wii", "white 12' with blue strobe led", "wii_0707", 850.00, 10, "~/Content/Images/NT/wii1.png", new DateTime(2003, 1, 20).Date, "2 working days", "switch_P23", "medium"));
                ntendos.Add(new Nintendo(2, "Game", "GameV console with chrome finish", "wii_888", 1500.00, 10, "~/Content/Images/NT/wii2.png", new DateTime(206, 1, 21).Date, "2 working days", "switch_P23", "small"));
                ntendos.Add(new Nintendo(3, "DS Lite ", "dual screen padded device", "wii_0ds900", 2400.00, 3, "~/Content/Images/NT/wii3.png", new DateTime(2010, 1, 24).Date, "3 working days", "switch_P23", "large"));
                ntendos.Add(new Nintendo(4, "SwitchT", "white 12' with blue strobe led", "wii_swtch_09", 3000.00, 1, "~/Content/Images/NT/wii4.png", new DateTime(2019, 1, 23).Date, "1 working days", "switch_P23", "medium"));
                //add mplaystations to store list
                StoreItems.AddRange(ntendos);

                //Redirect to home on successful creation
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //if objects already exist in shop the redirect to home 
                return RedirectToAction("Index", "Home");
            }
        }

        //Declare Store Item List to hold objects for shop 

        public static  List<ConsoleI> StoreItems = new List<ConsoleI>();

        //Declare Shop Cart List to hold items added to class

        public static List<ConsoleI> Cart = new List<ConsoleI>();

        public ActionResult Index()
        {
            //use to display messages to the user based on action performed using ViewBags
            var Msg = TempData["Message"] as string;

            if (Msg == null || Msg == "")
            {
                ViewBag.Message = "Welcome";
            }
            else
            {
                ViewBag.Message = Msg;
            }

            //Send count of cartitems to Home View
            ViewBag.CartCount = Cart.Count();
            return View(StoreItems);
        }

        //Action When Added to Cart
        public ActionResult AddItemToCart( /* Recive Name of Item to Add*/ string Name)
        {
            //Locate Specific item in Shop
            var ItemInShop = StoreItems.Find(item => item._Name == Name);

            //check the quantity of that item and prompt user if item is out of stock
            if(ItemInShop._Quantity == 0 || ItemInShop._Quantity < 0)
            {
                //if items quantity is low/ out of stock the let user know and redirect message to index 
                TempData["Message"] = "Unfortunatly the Product is out of stock ...Please checkback soon!!!";
                return RedirectToAction("Index","Home");
            }
            else if (ItemInShop._Quantity > 0 )
            {
                //if items overall quantity is greater than 0 then decrese its quantity by 1 and add it to the cart 
                ItemInShop._Quantity -= 1;
                ConsoleI addtocart = ItemInShop;
                Cart.Add(addtocart);
                TempData["Message"] = "Successfully added to Cart";
            }
            //regardless of wehat happens reroute back to index view
            return RedirectToAction("Index","Home");
        }

        //View Itemms in Cart
        public ActionResult ViewCart()
        {
            // Store Cart Item in Tempdata and send to cart Controller
            TempData["CartItems"] = Cart;
            return RedirectToAction("CartView", "Cart");
        }

        //Change quantity of desired item from Cart View
        public ActionResult PlusQuantity(string Name)
        {
            //Find Item
            var ItemInShop = StoreItems.Find(item => item._Name == Name);
            //check quantity
            if (ItemInShop._Quantity == 0 || ItemInShop._Quantity < 0)
            {
                TempData["Message"] = "Unfortunatly the Product is out of stock ...Please checkback soon!!!";
                return RedirectToAction("ViewCart", "Home");
            }
            else if (ItemInShop._Quantity > 0)
            {
                //change quantity
                ItemInShop._Quantity -= 1;
                ConsoleI addtocart = ItemInShop;
                Cart.Add(addtocart);
                TempData["Message"] = "Successfully added to Cart";

            }
            return RedirectToAction("ViewCart", "Home");
        }

        //Change quantity of desired item from Cart View
        public ActionResult MinusQuantity(string Name)
        {
            // Remove item form cart 
            var ItemInShop = StoreItems.Find(item => item._Name == Name);
            ItemInShop._Quantity += 1;
            ConsoleI removefromtocart = ItemInShop;
            Cart.Remove(removefromtocart);
            TempData["Message"] = "Successfully removed from Cart";

            //Route back to View Cart
            return RedirectToAction("ViewCart", "Home");
        }

        //Operation when clear Cart button is clicked in Cart Controller
        public ActionResult ResetShopItems()
        {
            playstations.Clear();
            xboxs.Clear();
            ntendos.Clear();
            StoreItems.Clear();
            Cart.Clear();
            return RedirectToAction("CreateIndex","Home");
        }

        //On checkout complete just clear the basket
        public ActionResult CheckoutClear()
        {

            Cart.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}