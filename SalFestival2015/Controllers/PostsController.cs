using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using PagedList;
using System.Web.Mvc;
using SalFestival2015.Models;

namespace SalFestival2015.Controllers
{

    public class PostsController : Controller
    {
        private salalahDBEntities db = new salalahDBEntities();

        // GET: Posts
        public ActionResult Index(int? page)
        {
            var posts = db.tbl_posts.OrderByDescending(x => x.Id).ToList(); ; //returns IQueryable<Posts> representing an unknown number of Posts. a thousand maybe?

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfPosts = posts.ToPagedList(pageNumber, 25); // will only contain 25 posts max because of the pageSize

            ViewBag.OnePageOfPosts = onePageOfPosts;
            return View();
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_posts tbl_posts = db.tbl_posts.Find(id);
            if (tbl_posts == null)
            {
                return HttpNotFound();
            }
            return View(tbl_posts);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,post_auid,post_title,post_date,year,month,post_content,post_excerpt,post_catid,post_modified,post_status,post_parent,post_menu_order,post_img,post_price")] tbl_posts tbl_posts)
        {
            if (ModelState.IsValid)
            {
                db.tbl_posts.Add(tbl_posts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_posts);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_posts tbl_posts = db.tbl_posts.Find(id);
            if (tbl_posts == null)
            {
                return HttpNotFound();
            }
            return View(tbl_posts);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,post_auid,post_title,post_date,year,month,post_content,post_excerpt,post_catid,post_modified,post_status,post_parent,post_menu_order,post_img,post_price")] tbl_posts tbl_posts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_posts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_posts);
        }

     [HttpPost]
        public ActionResult Delete(int id=0)
        {
            if (id !=0)
            {
                tbl_posts tbl_posts = db.tbl_posts.Where(x=>x.Id==id).FirstOrDefault();
                db.tbl_posts.Remove(tbl_posts);
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

        public ActionResult getPostDetails(int id)
        {
            var gPDetail = db.tbl_posts.Where(x => x.Id == id).FirstOrDefault();
            return View(gPDetail);
        }
    }
}
