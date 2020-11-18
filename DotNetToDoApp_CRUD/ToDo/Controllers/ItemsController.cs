using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class ItemsController : Controller
    {
        private tododB db = new tododB();

        public ActionResult Create(int? id)
        {
            ViewBag.catID = id;
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1([Bind(Include = "ID,Name,catID,Completed")] Item item, string catID)
        {
            if (ModelState.IsValid)
            {
                item.catID = Convert.ToInt32( catID);
                db.Items.Add(item);
                db.SaveChanges();
                TempData["Message"] = "Success";
                return RedirectToAction("Index","List");
            }

            ViewBag.catID = new SelectList(db.Categories, "catID", "Name", item.catID);
            return View(item);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.catID = new SelectList(db.Categories, "catID", "Name", item.catID);
            return View(item);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,catID,Completed")] Item item)
        {
            if (ModelState.IsValid)
            {
                TempData["Message"] = "The Item was successfully changed to " + "--" + "" + item.Name + "" + "--" + "!!";
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","List");
            }
            ViewBag.catID = new SelectList(db.Categories, "catID", "Name", item.catID);
            return View(item);
        }

    
        public ActionResult Delete(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            TempData["Message"] = "The Item" + "" + "--" + "" + item.Name + "" + "--" +  "was successfully deleted";
            return RedirectToAction("Index","List");
        }

        
        public ActionResult DeleteChecked()
        {
            List<Item> checkedItems = db.Items.Where(selc => selc.Completed == true).ToList();
            foreach ( var item in checkedItems)
            {
                db.Items.Remove(item);
                db.SaveChanges();              
            }
            TempData["Message"] = "The Completed Items have been Removed";
            return RedirectToAction("Index", "List");
        }

        public ActionResult Togglechecked(int? id)
        {
            var Toggleitem = db.Items.Find(id);
            if (Toggleitem.Completed == false)
            {
                Toggleitem.Completed = true;
            }
            else
            {
                Toggleitem.Completed = false;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "List");
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
