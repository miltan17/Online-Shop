using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop4.Controllers
{
    [Authorize(Users="admin")]
    public class AdminController : Controller
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View(db.ProductGalaries.ToList());
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(int id = 0)
        {
            var models = db.ProductGalaries.Find(id);
            return View(models);
        }

       

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProductGalary productgalary = db.ProductGalaries.Find(id);
            if (productgalary == null)
            {
                return HttpNotFound();
            }
            return View(productgalary);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(ProductGalary productgalary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productgalary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productgalary);
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ProductGalary productgalary = db.ProductGalaries.Find(id);
            if (productgalary == null)
            {
                return HttpNotFound();
            }
            return View(productgalary);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductGalary productgalary = db.ProductGalaries.Find(id);
            db.ProductGalaries.Remove(productgalary);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}