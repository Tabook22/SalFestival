using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalFestival2015.Models;
namespace SalFestival2015.Controllers
{

    public class ArticlesController : Controller
    {
        salalahDBEntities db = new salalahDBEntities();
        // GET: Admin/Articles

        public ActionResult artdetails(int id)
        {
            return View();
        }

        public ActionResult getArtDetails (int id)
        {
            var gADetail = db.tbl_articles.Where(x => x.Id == id).FirstOrDefault();
            return View(gADetail);
        }

        public ActionResult HeadLines()
        {
            salalahDBEntities db = new salalahDBEntities();
            var sheadlines= (from hl in db.tbl_articles 
                             where hl.a_loc == "12"
                             select hl);

            int getTotalNo = db.tbl_articles.Where(x => x.a_loc == "12").Count();

            ViewBag.getTotalNo = getTotalNo;

            return View(sheadlines);
        } 
    }
}