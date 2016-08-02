using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SalFestival2015.Models;
namespace SalFestival2015.Controllers
{

    public class DailyWinnersController : Controller
    {
        salalahDBEntities db = new salalahDBEntities();
        // GET: DailyWinners
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Winnerslist()
        {
            return View();
        }
         public ActionResult addWinner(int? page, int? q1, int? q2)
        {
            
 
            IEnumerable<tbl_gust_Answers> gstans = db.tbl_gust_Answers.Where(x => x.q_no == q1 && x.q_ans == q2).OrderBy(x => x.g_name).ToList();

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfWinners = gstans.ToPagedList(pageNumber, 50); // will only contain 25 posts max because of the pageSize

            ViewBag.GetWinners = onePageOfWinners;

           // IEnumerable<tbl_gust_Answers> gstans=db.tbl_gust_Answers.Where(x=>x.q_no==qno && x.q_ans ==qans).OrderBy(x=>x.g_name) .ToList ();
            //ViewBag.GetWinners = gstans;
            ViewBag.Q1 = q1;
            ViewBag.Q2 = q2;
            return View("addWinner");
            }

        [HttpPost]

       public ActionResult addWinner(int? page, FormCollection frm)
        {

                int qno = Convert.ToInt32(frm.Get("txtqno"));
                int qans = Convert.ToInt32(frm.Get("txtqans"));
                IEnumerable<tbl_gust_Answers> gstans = db.tbl_gust_Answers.Where(x => x.q_no == qno && x.q_ans == qans).OrderBy(x => x.g_name).ToList();

                var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
                var onePageOfWinners = gstans.ToPagedList(pageNumber, 50); // will only contain 25 posts max because of the pageSize

                ViewBag.GetWinners = onePageOfWinners;

                // IEnumerable<tbl_gust_Answers> gstans=db.tbl_gust_Answers.Where(x=>x.q_no==qno && x.q_ans ==qans).OrderBy(x=>x.g_name) .ToList ();
                //ViewBag.GetWinners = gstans;
                ViewBag.Q1 = qno;
                ViewBag.Q2 = qans;
                return View("addWinner");
           
        }

       public ActionResult radomwinners(int qno, int qans)
        {
           // here the extention method randomeNames where used to select random users

            var selection = db.tbl_gust_Answers.OrderBy (c =>c.g_name).Where(x=>x.q_no == qno && x.q_ans == qans).ToList();
            var randomNames = selection.RandomSample(3, false); //false means no duplicate are allowed
            ViewBag.getUsers = randomNames;
            return View("dwlst");
        }
    }
}