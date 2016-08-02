using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalFestival2015.Models;
namespace SalFestival2015.Controllers
{
    public class AdvController : Controller
    {
        salalahDBEntities db = new salalahDBEntities();
        // GET: Adv
        public ActionResult Index()
        {
            var maArt = (from dart in db.tbl_articles
                         where dart.a_loc == "6"
                         orderby dart.a_order ascending
                         select dart);
            return View(maArt);
        }

        public ActionResult siteAdv()
        {
            var sitAdv = (from dart in db.tbl_articles
                         where dart.a_loc == "6"
                         orderby dart.a_order ascending
                         select dart);
            return View(sitAdv);
        }

        public ActionResult topSiteAdv()
        {
            salalahDBEntities db = new salalahDBEntities();
            var sheadlines = (from hl in db.tbl_articles
                              where hl.a_loc == "1"
                              orderby hl.a_loc ascending
                              select hl);

            int getTotalNo = db.tbl_articles.Where(x => x.a_loc == "1").Count();

            ViewBag.getTotalNo = getTotalNo;

            return View(sheadlines);
        }
    }
}