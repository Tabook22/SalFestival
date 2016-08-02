using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalFestival2015.Models;
namespace SalFestival2015.Controllers
{
    
    public class AdminController : Controller
    {
        salalahDBEntities db = new salalahDBEntities();
        // GET: Admin
        public ActionResult Index()
        {
            //get total number of posts in the site
           ViewBag.postscounts=db.tbl_posts.OrderBy(x => x.Id).Count();

           //get total number of news and articles in the site
           ViewBag.artcount = db.tbl_articles.OrderBy(x => x.Id).Count();
            return View();
        }

    }
}