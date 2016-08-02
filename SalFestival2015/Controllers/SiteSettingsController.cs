using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SalFestival2015.Models;
using SalFestival2015.ViewModels;
namespace SalFestival2015.Controllers
{

    [RouteArea("Admin")] // the area
    [RoutePrefix("SiteSettings")] // the name of the controller
    [Route("{action=index}")] // using action is optional and the default is index
    public class SiteSettingsController : Controller
    {
        salalahDBEntities db = new salalahDBEntities();
        // GET: Admin/SiteSettings
        public ActionResult Index()
        {
            using (db)
            {
                IEnumerable<tbl_articles> myart = from ar in db.tbl_articles
                                                  select ar;
                return View(myart.ToList());
            }

        }


        //Get: ArticleSetting
        //public ActionResult ArticleSettings()
        //{
        //    ViewBag.SectionsName2 = new SelectList(db.tbl_siteSections, "secId", "sec_title");
        //    return View(db.tbl_articles.OrderBy(x => x.a_order).ToList());
        //}


        public ActionResult getArtLst(int? page, string artid)
        {
            IEnumerable<tbl_articles> myArt = db.tbl_articles.OrderBy(x => x.a_order).Where(x => x.a_loc.Contains(artid)).ToList();
            ViewBag.Message = "Deployments";
            ViewBag.PageSize = 10;
            ViewBag.TotalRecords = db.tbl_articles.OrderBy(x => x.a_order).Where(x => x.a_loc.Contains(artid)).Count() ;

            if (Request.IsAjaxRequest())
                return PartialView("_artlist", myArt);
            else
                return View("_artlist", myArt);
        }
        public ActionResult ArticleSettings()
        {

            // this is used to fill the dropdownlist
            //var mydata = from se in db.tbl_siteSections
            //             select new { se.secId, se.sec_title };
            //SelectList mylist = new SelectList(mydata, "secId", "sec_title");
            ViewBag.SectionsName2 = new SelectList(db.tbl_siteSections, "secId", "sec_title");
            //ViewBag.mysections = mylist;


            //ViewBag.sections = new SelectList(db.tbl_siteSections, "secId", "sec_title");

            //getting the article list ordered by the a_order field

            //var getartlist = from art in db.tbl_articles
            //                 orderby art.a_order ascending
            //                 select art;
            
            return View();
        }
        //Update Article settings
        [HttpPost]
        public ActionResult upArticleSettings(tbl_articles article, FormCollection form)
        {
            int getId = Convert.ToInt32(article.Id);

            if (ModelState.IsValid)
            {
                using (db)
                {
                    tbl_articles tbl_art = new tbl_articles();
                    tbl_art = db.tbl_articles.Where(x => x.Id == getId).FirstOrDefault();
                    tbl_art.a_date = DateTime.Now;
                    tbl_art.a_link = article.a_link;
                    tbl_art.a_title = article.a_title;
                    tbl_art.a_desc = article.a_desc;
                    tbl_art.a_loc = form["mysections"];
                    tbl_art.a_img = article.a_img;
                    tbl_art.a_order = article.a_order;
                    tbl_art.a_status = article.a_status;
                    db.SaveChanges();
                }

            }
            return RedirectToAction("ArticleSettings");
        }
        //Post: ArticleSetting
        [HttpPost]
        public ActionResult ArticleSettings(Articles article, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                tbl_articles tbl_article = new tbl_articles();
                tbl_article.a_date = DateTime.Now;
                tbl_article.a_link = article.a_link;
                tbl_article.a_title = article.a_title;
                tbl_article.a_desc = article.a_desc;
                tbl_article.a_loc = article.a_loc;
                tbl_article.a_img = article.a_img;
                tbl_article.a_order = article.a_order;
                tbl_article.a_status = article.a_status;

                db.tbl_articles.Add(tbl_article);
                db.SaveChanges();


            }
            return RedirectToAction("Index");
        }

        //Get: Site Sections
        [Route("createSiteSec")]
        public ActionResult createSiteSec()
        {
            return View();
        }

        //Post: Site Sections
        [HttpPost]
        public ActionResult createSiteSec(SiteSections sitsc, FormCollection form)
        {
            using (db)
            {
                if (ModelState.IsValid)
                {
                    tbl_siteSections secst = new tbl_siteSections();
                    secst.sec_title = sitsc.sec_title;
                    secst.sec_lock = form["mysec"];
                    secst.sec_desc = sitsc.sec_desc;
                    secst.sec_status = sitsc.sec_status;
                    db.tbl_siteSections.Add(secst);
                    db.SaveChanges();
                }
                //ModelState.Clear();
                return RedirectToAction("Sections");
            }

        }

        //Get: Section list
        public ActionResult SecList()
        {
            using (db)
            {
                int sct = db.tbl_siteSections.SqlQuery("select * from tbl_siteSections").Count();
                var getSecLst = db.tbl_siteSections.ToList();
                return View("SecList", getSecLst);
            }
        }

        //Post: Section Delete
        //[HttpPost]
        //[Route("secDelete/{secid?}")]
        public ActionResult secDelete(int id)
        {

            //db.tbl_siteSections.Remove(db.tbl_siteSections.Where(x => x.secId == secid).FirstOrDefault());
            //db.SaveChanges();
            tbl_siteSections mysec = db.tbl_siteSections.Find(id);
            if (mysec == null)
            {
                return HttpNotFound();
            }
            db.tbl_siteSections.Remove(mysec);
            db.SaveChanges();
            return RedirectToAction("Sections");
        }

        [HttpPost]
        public ActionResult secEdit(string id)
        {

            int sid = Convert.ToInt32(id);

            int mysec = db.tbl_siteSections.Where(x => x.secId == sid).Count();
            if (mysec != null)
            {
                tbl_siteSections mysections = db.tbl_siteSections.Where(x => x.secId == sid).FirstOrDefault();
                return Json(mysections, JsonRequestBehavior.AllowGet);
            }
            return Json("No value found", JsonRequestBehavior.AllowGet);

        }

        public PartialViewResult createNewSection()
        {
          
            ViewBag.mysec = new SelectList(db.tbl_mainsections, "id", "secdesc");
            return PartialView("_createNewSection");
        }

        //here we to show the editable data so we can change it and then update
        public PartialViewResult editSection(int id)
        {

            var viewModel = new SiteSections();
            tbl_siteSections mysec = db.tbl_siteSections.Where(x => x.secId == id).FirstOrDefault();

            ViewBag.mysections = new SelectList(db.tbl_mainsections, "id", "secdesc", mysec.sec_lock);
            return PartialView("_editSection", mysec);

        }

        // Edit articles which used in the section inside the website
        public PartialViewResult editart(int id)
        {

            // this is use to fill the dropdownlist
            //var mydata = from se in db.tbl_siteSections
            //             select new { se.secId, se.sec_title };
            //SelectList mylist = new SelectList(mydata, "secId", "sec_title");
            //ViewBag.mysections = mylist;



            //here we want to keep the same items which were selected in the dropdownlist and show them as a selected item
            tbl_articles myart = db.tbl_articles.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.mysections = new SelectList(db.tbl_siteSections, "secId", "sec_title", myart.a_loc);
            return PartialView("_editart", myart);

        }

        // Add new articles or news to the site sections
        //public PartialViewResult addart()
        //{

        //    ViewBag.SectionsName = new SelectList(db.tbl_siteSections, "secId", "sec_title");
        //    return PartialView("_addart");
        //}

        public ActionResult addart()
        {

            ViewBag.SectionsName = new SelectList(db.tbl_siteSections, "secId", "sec_title");
            return View("addart");
        }
        // Post adding new article or news to the site sections
        [HttpPost]
        public ActionResult addart(tbl_articles article, FormCollection form)
        {
            //int getId = Convert.ToInt32(article.Id);

            if (ModelState.IsValid)
            {
                using (db)
                {
                    tbl_articles tbl_art = new tbl_articles();
                    //tbl_art = db.tbl_articles.Where(x => x.Id == getId).FirstOrDefault();
                    tbl_art.a_date = DateTime.Now;
                    tbl_art.a_link = article.a_link;
                    tbl_art.a_title = article.a_title;
                    tbl_art.a_desc = article.a_desc;
                    tbl_art.a_loc = form["SectionsName"];
                    tbl_art.a_img = article.a_img;
                    tbl_art.a_order = article.a_order;
                    tbl_art.a_status = article.a_status;
                    db.tbl_articles.Add(tbl_art);
                    db.SaveChanges();
                }

            }
            return RedirectToAction("ArticleSettings");
        }

        // Delete articles
        public ActionResult DeleteArticles(string[] ids)
        {
            //Delete Selected
            int[] id = null;
            if (ids != null)
            {
                id = new int[ids.Length];
                int j = 0;
                foreach (string i in ids)
                {
                    int.TryParse(i, out id[j++]);

                }
            }
            if (id != null && id.Length > 0)
            {
                List<tbl_articles> allSelected = new List<tbl_articles>();
                allSelected = db.tbl_articles.Where(x => id.Contains(x.Id)).ToList();
                foreach (var i in allSelected)
                {
                    db.tbl_articles.Remove(i);

                }
                db.SaveChanges();
            }
            return RedirectToAction("ArticleSettings");
        }

        //this is where we update the contents of the sections
        public ActionResult editSectionToDatabase(tbl_siteSections mysec, FormCollection form)
        {
            int getId = Convert.ToInt32(mysec.secId);

            if (ModelState.IsValid)
            {
                using (db)
                {
                    tbl_siteSections tbl_siteSection = new tbl_siteSections();
                    tbl_siteSection = db.tbl_siteSections.Where(x => x.secId == getId).FirstOrDefault();
                    tbl_siteSection.sec_title = mysec.sec_title;
                    tbl_siteSection.sec_title = mysec.sec_title;
                    tbl_siteSection.sec_lock = form["mysections"];
                    tbl_siteSection.sec_desc = mysec.sec_desc;
                    tbl_siteSection.sec_status = mysec.sec_status;
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Sections");
        }

        public ActionResult Sections()
        {
            List<tbl_siteSections> mysec = new List<tbl_siteSections>();
            using (db)
            {
                mysec = db.tbl_siteSections.ToList();
                return View(mysec);
            }

        }

        public ActionResult DeleteSelected(string[] ids)
        {
            //Delete Selected
            int[] id = null;
            if (ids != null)
            {
                id = new int[ids.Length];
                int j = 0;
                foreach (string i in ids)
                {
                    int.TryParse(i, out id[j++]);

                }
            }
            if (id != null && id.Length > 0)
            {
                List<tbl_siteSections> allSelected = new List<tbl_siteSections>();
                allSelected = db.tbl_siteSections.Where(x => id.Contains(x.secId)).ToList();
                foreach (var i in allSelected)
                {
                    db.tbl_siteSections.Remove(i);

                }
                db.SaveChanges();
            }
            return RedirectToAction("Sections");
        }

       
    }
}