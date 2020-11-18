using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.Models;

namespace ToDo.Controllers
{

    public class ListController : Controller
    {
        private tododB db = new tododB();

        // GET: List
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            List<Category> Categories = db.Categories.ToList();
            List<Item> Items = db.Items.ToList();

            string fullPath = Server.MapPath("~/TxtFiles/");
            string fullPath1 = Server.MapPath("~/TxtFiles/");

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(fullPath, "Categories.txt")))
            {
                foreach (var line in Categories)
                    outputFile.WriteLine(Convert.ToString(line.catID) + "," + Convert.ToString(line.Name));
            }

            using (StreamWriter outputFile1 = new StreamWriter(Path.Combine(fullPath1, "Items.txt")))
            {
                foreach (var line in Items)
                    outputFile1.WriteLine(Convert.ToString(line.Name) + "," + Convert.ToString(line.ID) + "," + Convert.ToString(line.catID) + "," + Convert.ToString(line.Completed));
            }

            var Msg = TempData["Message"] as string;

            if (Msg == null || Msg == "")
            {
                ViewBag.Message = "Welcome";
            }
            else
            {
                ViewBag.Message = Msg;

            }
            return View(db.Categories.ToList());
        }

    }
}
