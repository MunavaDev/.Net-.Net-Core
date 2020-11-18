using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class CategoriesController : Controller
    {
        private tododB db = new tododB();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "catID,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                
                db.Categories.Add(category);
                db.SaveChanges();
                TempData["Message"] = "Success";
                return RedirectToAction("Index","List");
            }

            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "catID,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                TempData["Message"] = "The Category was successfully changed to " + "--" +"" +  category.Name + ""+ "--" + "!!";
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "List");
            }
            return View(category);
        }

        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            TempData["Message"] = "The Category" +"" + "--" + "" + category.Name + "" + "--" + "was successfully deleted";
            return RedirectToAction("Index","List");
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
