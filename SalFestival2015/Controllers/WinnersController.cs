using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SalFestival2015.Models;

namespace SalFestival2015.Controllers
{

    public class WinnersController : Controller
    {
        private salalahDBEntities db = new salalahDBEntities();

        // GET: Winners
        public ActionResult Index(int? page)
        {
            var winners = db.tbl_winners.OrderByDescending(x => x.g_date).ToList(); ; //returns IQueryable<Posts> representing an unknown number of Posts. a thousand maybe?

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfWinners = winners.ToPagedList(pageNumber, 30); // will only contain 25 posts max because of the pageSize

            ViewBag.OnePageOfWinners = onePageOfWinners;
            return View();
        }

        // GET: Winners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_winners tbl_winners = db.tbl_winners.Find(id);
            if (tbl_winners == null)
            {
                return HttpNotFound();
            }
            return View(tbl_winners);
        }

        // GET: Winners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Winners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_winners tbl_winners, FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                tbl_winners.g_date = DateTime.Now;
                tbl_winners.g_name = frm["g_name"];
                tbl_winners.g_email = frm["g_email"];
                tbl_winners.g_tel = frm["g_tel"];
                tbl_winners.q_no = Convert.ToInt32(frm["q_no"]);
                tbl_winners.q_ans = Convert.ToInt32 (frm["q_ans"]);

                db.tbl_winners.Add(tbl_winners);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_winners);
        }

        // GET: Winners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_winners tbl_winners = db.tbl_winners.Find(id);
            if (tbl_winners == null)
            {
                return HttpNotFound();
            }
            return View(tbl_winners);
        }

        // POST: Winners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,g_date,g_name,g_email,g_tel,q_no,q_ans")] tbl_winners tbl_winners)
        {
            if (ModelState.IsValid)
            {
                tbl_winners.g_date = DateTime.Now;
                db.Entry(tbl_winners).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_winners);
        }

        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            if (id != 0)
            {
                tbl_winners tbl_winners = db.tbl_winners.Where(x => x.Id == id).FirstOrDefault();
                db.tbl_winners.Remove(tbl_winners);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }

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
