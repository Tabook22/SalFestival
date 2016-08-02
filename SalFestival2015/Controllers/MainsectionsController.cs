using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalFestival2015.Models;

namespace SalFestival2015.Controllers
{
    public class MainsectionsController : Controller
    {
        private salalahDBEntities db = new salalahDBEntities();

        // GET: Mainsections
        public ActionResult Index()
        {
            return View(db.tbl_mainsections.ToList());
        }

        // GET: Mainsections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_mainsections tbl_mainsections = db.tbl_mainsections.Find(id);
            if (tbl_mainsections == null)
            {
                return HttpNotFound();
            }
            return View(tbl_mainsections);
        }

        // GET: Mainsections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mainsections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,secdesc")] tbl_mainsections tbl_mainsections)
        {
            if (ModelState.IsValid)
            {
                db.tbl_mainsections.Add(tbl_mainsections);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_mainsections);
        }

        // GET: Mainsections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_mainsections tbl_mainsections = db.tbl_mainsections.Find(id);
            if (tbl_mainsections == null)
            {
                return HttpNotFound();
            }
            return View(tbl_mainsections);
        }

        // POST: Mainsections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,secdesc")] tbl_mainsections tbl_mainsections)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_mainsections).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_mainsections);
        }

        // GET: Mainsections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_mainsections tbl_mainsections = db.tbl_mainsections.Find(id);
            if (tbl_mainsections == null)
            {
                return HttpNotFound();
            }
            return View(tbl_mainsections);
        }

        // POST: Mainsections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_mainsections tbl_mainsections = db.tbl_mainsections.Find(id);
            db.tbl_mainsections.Remove(tbl_mainsections);
            db.SaveChanges();
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
