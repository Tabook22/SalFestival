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
using System.IO;
using System.Web.Script.Serialization;

namespace SalFestival2015.Controllers
{

    public class QuizeController : Controller
    {
        private salalahDBEntities db = new salalahDBEntities();

        // GET: Quize
        public ActionResult Index(int? page)
        {
            var posts = db.tbl_quize.OrderByDescending(x => x.Id).ToList(); ; //returns IQueryable<Posts> representing an unknown number of Posts. a thousand maybe?

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfPosts = posts.ToPagedList(pageNumber, 25); // will only contain 25 posts max because of the pageSize

            ViewBag.OnePageOfPosts = onePageOfPosts;
            return View();
        }

        // GET: Quize/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_quize tbl_quize = db.tbl_quize.Find(id);
            if (tbl_quize == null)
            {
                return HttpNotFound();
            }
            return View(tbl_quize);
        }

        // GET: Quize/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quize/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,q_date,q_no,q_question,q_ans1,q_ans2,q_ans3,q_ans4,q_corr,q_img,q_img_show")] tbl_quize tbl_quize)
        {
            if (ModelState.IsValid)
            {
                db.tbl_quize.Add(tbl_quize);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_quize);
        }

        // GET: Quize/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_quize tbl_quize = db.tbl_quize.Find(id);
            if (tbl_quize == null)
            {
                return HttpNotFound();
            }
            return View(tbl_quize);
        }

        // POST: Quize/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,q_date,q_no,q_question,q_ans1,q_ans2,q_ans3,q_ans4,q_corr,q_img,q_img_show")] tbl_quize tbl_quize)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_quize).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_quize);
        }

        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            if (id != 0)
            {
                tbl_quize tbl_quize = db.tbl_quize.Where(x => x.Id == id).FirstOrDefault();
                db.tbl_quize.Remove(tbl_quize );
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

        // this action will display the question
        //Get
        public ActionResult DisplayQuize()
        {
            
            var getQ = db.tbl_quize.Where(x => x.q_img_show == 1).ToList();
            return View(getQ);
        }


        [HttpPost]
        public ActionResult DisplayQuize(FormCollection frm)
        {
            return View();
        }

        // here the guest will add their daily quize into the database
        public ActionResult AddDialyQuize()
        {

            return View();
        }

        //[HttpPost]
        //public ActionResult AddDialyQuize(FormCollection frm)
        public ActionResult AddDialyQuize2(string date, string name, string email, string tel, string qans, string qno)
        {
            //            //ctsApp capp = new ctsApp();
            //            List<QuizeFestival> lstquize = new List<QuizeFestival>();
            //            QuizeFestival quize = new QuizeFestival();
            //            quize.date = DateTime.Now;
            //            quize.name = name;
            //            quize.email = email;
            //            quize.tel = tel;
            //            quize.qans =qans;
            //            quize.qno = qno;


            //            lstquize.Add(quize);


            //            //get the Json filepath  
            //            string file = Server.MapPath("~//uploads/quize.json");
            //            //deserialize JSON from file  
            //            string Json = System.IO.File.ReadAllText(file);
            //            JavaScriptSerializer ser = new JavaScriptSerializer();
            //            var personlist = ser.Deserialize<List<QuizeFestival>>(Json);

            //            if (personlist !=null)
            //            {
            ////here we are adding the new added data to the previous ones
            //            foreach (var itm in personlist)
            //            {
            //                lstquize.Add(itm);
            //            }
            //            }

            //            // retrive the data from table  

            //            // Pass the "personlist" object for conversion object to JSON string  
            //            string jsondata = new JavaScriptSerializer().Serialize(lstquize);
            //            string path = Server.MapPath("~/uploads/");
            //            // Write that JSON to txt file,  
            //            System.IO.File.WriteAllText(path + "quize.json", jsondata);
            //            TempData["msg"] = "لقد تم إضافة  الإيجابة بنجاح";
            //            return RedirectToAction("DisplayQuize");





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
            //return Redirect("http://iramoman.com");
            return Redirect("http://altersanah.com/home/AddDialyQuize/?date="+DateTime.Now+"&name="+name+"&email="+email+"&tel="+tel+"&qans="+qans+"&qno="+qno);
            //return Redirect("http://iramoman.com/home/AddDialyQuize/?date=" + DateTime.Now + "&name=" + "11111" + "&email=" + "2222" + "&tel=" + "33333" + "&qans=" + "444444" + "&qno=" + "55555");
        }
        //?artistName=cher&apiKey=XXX
        public ActionResult QThanks()
        {
            return View();
        }

        public ActionResult DownLoadFile()
        {
            var filename = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + "log\\" + "logErrors.txt";
            var sw = new System.IO.StreamWriter(filename, true);
            sw.WriteLine(DateTime.Now.ToString() + " " + "ddddddddd" + " " +"rrrrrrrrrrrrrrrr");
            sw.Close();

            return View("Index");

        }
    }
}
