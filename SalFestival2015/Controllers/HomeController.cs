using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalFestival2015.Models;
using SalFestival2015.ViewModels;
using System.Web.Security;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace SalFestival2015.Controllers
{
    public class HomeController : Controller
    {
        salalahDBEntities db = new salalahDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index_old()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DArticles()
        {
            var darticles = (from dart in db.tbl_articles
                             where dart.a_loc == "2"
                             orderby dart.a_order
                             select dart);

            return View("_mainarticles", darticles);
        }

        // here we are selecting the top 14 image from the articles to be displayed in the first image
        public ActionResult getImgGal()
        {
            var imgticles = (from dart in db.tbl_articles
                             where dart.a_loc == "1"
                             orderby dart.a_order ascending
                             select dart).Take(9);

            return View(imgticles);
        }


        public ActionResult getMainArt()
        {
            var maArt = (from dart in db.tbl_articles
                         where dart.a_loc == "4"
                         orderby dart.a_order ascending
                         select dart);
            return View("_mainart", maArt);
        }
        public ActionResult getSideAdv()
        {
            var maArt = (from dart in db.tbl_articles
                         where dart.a_loc == "6"
                         orderby dart.a_order ascending
                         select dart);
            return View(maArt);
        }


        public ActionResult gettopAdv()
        {
            var maTopAdv = (from dart in db.tbl_articles
                            where dart.a_loc == "12"
                            orderby dart.a_order ascending
                            select dart).Take(1);
            return View(maTopAdv);
        }

        public ActionResult dailyevents()
        {
            var dEvents = (from dart in db.tbl_articles
                           where dart.a_loc == "8"
                           orderby dart.Id descending , dart.a_order ascending
                           select dart);
            foreach (var item in dEvents)
            {
                ViewBag.AdvId = item.Id;
            }

            ViewBag.DownloadFile = Server.MapPath("~/uploads/festival2015.pdf");
            return View(dEvents);
        }

        // getting the adv details

        public ActionResult GetPostData(int id)
        {
            var maArt = (from dart in db.tbl_articles
                         where dart.Id == id && dart.a_loc == "6"
                         orderby dart.a_order ascending
                         select dart);

            return PartialView("_sideadv", maArt);
            //return PartialView("_dataList");
        }


        // News Carousel

        public ActionResult newCarouse()
        {
            return View();
        }

      //shooting contest application

      public ActionResult contestApp()
        {
            return View();
        }
    
        [HttpPost]
        public ActionResult addcont(FormCollection form)
        {
            //ctsApp capp = new ctsApp();
            List<ctsApp> lstcapp = new List<ctsApp>();
           ctsApp capp = new ctsApp();
            capp.Cdate = DateTime.Now;
            capp.Name = Request.Form["name"];
            capp.IdNo = Request.Form["idno"];
            capp.State = Request.Form["state"];
            capp.Tel = Request.Form["tel"];
            capp.Contest = Request.Form["Contest"];


            lstcapp.Add(capp);


            //get the Json filepath  
            string file = Server.MapPath("~//uploads/output.json");
            //deserialize JSON from file  
            string Json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var personlist = ser.Deserialize<List<ctsApp>>(Json);

            //here we are adding the new added data to the previous ones
            foreach (var itm in personlist)
            {
                lstcapp.Add(itm);
            }
            // retrive the data from table  

            // Pass the "personlist" object for conversion object to JSON string  
            string jsondata = new JavaScriptSerializer().Serialize(lstcapp);
            string path = Server.MapPath("~/uploads/");
            // Write that JSON to txt file,  
            System.IO.File.WriteAllText(path + "output.json", jsondata);
            TempData["msg"] = "لقد تم إضافة الطلب بنجاح";
            return RedirectToAction("contestApp");
        }

        public ActionResult contestlist()
        {
            var getApps = db.cstapps.OrderBy(x => x.Name).ToList();
            return View(getApps);
        }

         //public ActionResult AddDialyQuize(FormCollection frm)
        public ActionResult AddDialyQuize(string date, string name, string email, string tel, string qans, string qno)
        {
            tbl_gust_Answers gans = new tbl_gust_Answers();
            gans.g_date = DateTime.Now;
            gans.g_name =name;
            gans.g_email = email;
            gans.g_tel = tel;
            gans.q_ans =  Convert.ToInt32(qans);
            gans.q_no =  Convert.ToInt32(qno);

            db.tbl_gust_Answers.Add(gans);
            db.SaveChanges();
            return Redirect("http://www.salalahfestival.gov.om/quize/qthanks");

            //var filename = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + "log\\" + "logErrors.txt";
            //var sw = new System.IO.StreamWriter(filename, true);
            //sw.WriteLine("Date: " + date + " ; Name: " + name+ " ; Email: " + email + " ; Tel: " + tel + " ; Answer: " + qans + " ; Question: " + qno);
            //sw.Close();
            ////return View("QThanks");
           // return Redirect("http://www.salalahfestival.gov.om/quize/qthanks");
        }

   
        public ActionResult AddcontApp(string date, string name, string idno, string state, string tel, string contest)
        {

            //tbl_gust_Answers gans = new tbl_gust_Answers();
            //gans.g_date = DateTime.Now;
            //gans.g_name = frm["g_name"];
            //gans.g_email = frm["g_email"];
            //gans.g_tel = frm["g_tel"];
            //gans.q_ans =  Convert.ToInt32(frm["q_ans"]);
            //gans.q_no =  Convert.ToInt32(frm["q_no"]);

            //db.tbl_gust_Answers.Add(gans);
            //db.SaveChanges();
            //return View("QThanks");

            //var filename = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + "log\\" + "logErrors.txt";
            //var sw = new System.IO.StreamWriter(filename, true);
            //sw.WriteLine("Date: "+DateTime.Now.ToString() + " , Name: " + frm["g_name"] + " , Email: " + frm["g_email"] + " , Tel: " + frm["g_tel"] + " , Answer: " + Convert.ToInt32(frm["q_ans"]) + " , Question: " + Convert.ToInt32(frm["q_no"]));
            //sw.Close();
            //return View("QThanks");
           // return Redirect("http://www.google.com");
           // return Redirect("http://altersanah.com");
            return Redirect("http://altersanah.com/home/AddContest/?name=" +name+"&idno="+idno+"&state="+state+"&tel="+tel+"&contest="+contest);
            //return Redirect("http://iramoman.com/home/AddDialyQuize/?date=" + DateTime.Now + "&name=" + "11111" + "&idno=" + "2222" + "&state=" + "33333" + "&tel=" + "444444" + "&contest=" + "55555");
        }

        public ActionResult SnapChatFes()
        {
            return View();
        }
    }
}