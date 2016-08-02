using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using SalFestival2015.ViewModels;
using System.IO;
using SalFestival2015.Models;
using ImageResizer;
namespace SalFestival2015.Controllers
{
  
    [RouteArea("Admin")] // the area
    [RoutePrefix("ImageUpload")] // the name of the controller
    [Route("{action=index}")] // using action is optional and the default is index
    public class ImageUploadController : Controller
    {

        private salalahDBEntities  db = new salalahDBEntities ();
        // GET: Admin/ImageUpload
        public ActionResult Index()
        {
            return View();
        }
        [Route("nasser/{nn?}")]
        public ActionResult nasser(string nn)
        {
            return Content("الحمد لله رب العالمين");
        }
        // this is the actio fro returning the partial view which is use going to be using it in the modal
        public ActionResult ViewLyubomir()
        {
            return PartialView("_Lyubomir");
        }
        [HttpPost]
        public ActionResult Lyubomir(folders folder)
        {
            string strMappath = Server.MapPath("~/UploadImages/" + folder.foldername);
            if (!Directory.Exists(strMappath))
            {
                DirectoryInfo di = Directory.CreateDirectory(strMappath);
            }
            return RedirectToAction("UploadImages");
        }

        public ActionResult ViewLyubomir2()
        {
            //TO:DO
            //here am going to list all the sub-foldders in the image foldder

            string folderPath = Server.MapPath("~/UploadImages/");
            List<string> picFolders = new List<string>();
            DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/UploadImages/"));
            DirectoryInfo[] directories = directory.GetDirectories();

            // your code check

            List<folders> myfolder = new List<folders>();

            foreach (DirectoryInfo folder in directories)
            {
                myfolder.Add(new folders { foldername = folder.Name });
            }

            return PartialView("_Lyubomir2", myfolder);
        }
        [Route("Lyubomir2/{deleteInputs?}")]
        public ActionResult Lyubomir2(string deleteInputs)
        {
            // You have your books IDs on the deleteInputs array
            if (deleteInputs != null && deleteInputs.Length > 0)
            {
                // And finally, redirect to the action that lists the books
                //(let's assume it's Index)
                // string strfolder = Request.Form["myFolders"].ToString();
                string strMappath = Server.MapPath("~/UploadImages/" + deleteInputs);
                if (Directory.Exists(strMappath))
                {
                    Directory.Delete(strMappath);
                }
                else
                {
                    return RedirectToAction("UploadImages");
                }
            }
            // And finally, redirect to the action that lists the books
            // (let's assume it's Index)
            //string strfolder = Request.Form["myFolders"].ToString();
            //string strMappath = Server.MapPath("~/UploadImages/" + strfolder);
            //if (Directory.Exists(strMappath))
            //{
            //    Directory.Delete(strMappath);
            //}
            return RedirectToAction("UploadImages");
        }
        // Get
        [Route("UploadImages")]
        public ActionResult UploadImages()
        {
            //TO:DO
            //here am going to list all the sub-foldders in the image foldder

            string folderPath = Server.MapPath("~/UploadImages/");
            List<string> picFolders = new List<string>();
            DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/UploadImages/"));
            DirectoryInfo[] directories = directory.GetDirectories();

            // your code check


            foreach (DirectoryInfo folder in directories)
            {
                picFolders.Add(folder.Name);
            }
            ViewData["folders"] = picFolders.ToList();


            SelectList folders = new SelectList(picFolders);

            ViewData["Folders"] = folders;
            return View();
        }

        //Upload New Image
        [HttpPost]
        public ActionResult UploadImages(ImgUpload imgd, HttpPostedFileBase file, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    ModelState.AddModelError("File", "الرجاء إعادة تحميل الصورة");
                }
                else if (file.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 3; //3 MB
                    //int MaxContentLength = 250;
                    string[] AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".gif", ".png", ".pdf" };

                    if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "الملفات التالية مسموح برفعها فقط: " + string.Join(", ", AllowedFileExtensions));
                    }

                    else if (file.ContentLength > MaxContentLength)
                    {
                        ModelState.AddModelError("File", "الملف الذي تقوم برفعه كبير الحجم المسموح برفعه هو:  " + MaxContentLength + " MB");
                    }
                    else
                    {
                        string getw;
                        string geth;
                        //here we check to see if the values of the given width and height are empty or not
                        if (String.IsNullOrEmpty(imgd.imgw))
                        {
                            getw = "600";
                        }
                        else
                        {
                            getw = imgd.imgw;
                        }
                        if (String.IsNullOrEmpty(imgd.imgh))
                        {
                            geth = "400";
                        }
                        else
                        {
                            geth = imgd.imgh;
                        }

                        String getfld = form["myFolders"].ToString();
                        //var path = Path.Combine(Server.MapPath("~/UploadImages/Uploads"));

                        var job = new ImageJob(file, "~/UploadImages/" + getfld + "/<guid>", new Instructions("mode=max;format=jpg;width=" + getw + ";height=" + geth + ";"));
                        job.CreateParentDirectory = true;
                        job.AddFileExtension = true;
                        job.Build();
                        string fileName = Path.GetFileName(job.FinalPath);
                        ViewData["ImageUrl"] = Url.Content("~/UploadImages/" + getfld + "/" + fileName);
                        //ViewBag.ImageUrl = Url.Content("~/UploadImages/" + getfld + "/" + fileName);
                        //ViewBag.FileName = fileName;


                        // var fileName = Path.GetFileName(file.FileName);
                        // string newImgName; // this will hold the new image name if the uploaded image exists in the database
                        string extension = Path.GetExtension(fileName);

                        //var path = Path.Combine(Server.MapPath("~/UploadImages/Uploads"), fileName);

                        using (salalahDBEntities db = new salalahDBEntities())
                        {

                            tbl_images1 tbl_image = new tbl_images1();
                            //first we check to see if the image is exists or not
                            tbl_image.imgfolder = getfld;
                            tbl_image.imgfullurl = Url.Content("~/UploadImages/" + getfld + "/" + fileName);
                            if (string.IsNullOrEmpty(imgd.imgtitle))
                            {
                                tbl_image.imgtitle = "لا يوجد عنوان";
                                ViewBag.FileName = "لا يوجد عنوان";
                            }
                            else
                            {
                                tbl_image.imgtitle = imgd.imgtitle;
                                ViewBag.FileName = imgd.imgtitle;
                            }
                            if (string.IsNullOrEmpty(imgd.imgdesc))
                            {
                                tbl_image.imgdesc = "لا يوجد وصف للصورة";
                                ViewBag.Desc = "لا يوجد وصف للصورة";
                            }
                            else
                            {
                                tbl_image.imgdesc = imgd.imgdesc;
                                ViewBag.Desc = imgd.imgdesc;
                            }
                            int getImg = db.tbl_images1.Where(x => x.imgurl_lg == file.FileName).Count(); // checing to see if there is anothr image with same name
                            if (getImg > 0)
                            {
                                //newImgName = fileName.Substring(0, 10);// taking the first 10 characters
                                //newImgName = newImgName + extension;
                                //var path1 = Path.Combine(Server.MapPath("~/UploadImages/Uploads"), fileName );
                                //file.SaveAs(path1);
                                tbl_image.imgurl_lg = fileName;

                                //ViewBag.imgName = path1;
                            }
                            else
                            {
                                //newImgName =fileName.Substring(0, 10);
                                //newImgName = newImgName + extension;
                                //var path1 = Path.Combine(Server.MapPath("~/UploadImages/Uploads"), fileName);
                                tbl_image.imgurl_lg = fileName;
                                //file.SaveAs(path1);
                                //ViewBag.imgName = path1;
                            }
                            tbl_image.imgdate = DateTime.Now;
                            tbl_image.imglink = imgd.imglink;
                            ViewBag.imglink = imgd.imglink;
                            tbl_image.imgw = getw;
                            tbl_image.imgh = geth;

                            db.tbl_images1.Add(tbl_image);
                            db.SaveChanges();
                        }
                        ModelState.Clear();
                        ViewBag.Message = "تم تحميل الصورة بنجاح";
                        TempData["imgName"] = fileName;// used to display the image

                    }
                }
            }
            //return RedirectToAction("UploadImages", "ImageUpload");
            //TO:DO
            //here am going to list all the sub-foldders in the image foldder

            string folderPath = Server.MapPath("~/UploadImages/");
            List<string> picFolders = new List<string>();
            DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/UploadImages/"));
            DirectoryInfo[] directories = directory.GetDirectories();

            // your code check


            foreach (DirectoryInfo folder in directories)
            {
                picFolders.Add(folder.Name);
            }
            ViewData["folders"] = picFolders.ToList();


            SelectList folders = new SelectList(picFolders);

            ViewData["Folders"] = folders;
            return View();
        }
        //Get
        //Edit Images
        public ActionResult EditImages(string imgurl, string imgfolder)
        {
            ViewBag.foldern = imgfolder; // this will hold the folder name 
            using (db)
            {
                tbl_images1 myimg = db.tbl_images1.Where(x => x.imgfullurl == imgurl).FirstOrDefault();
                ViewBag.imgId = myimg.imgid;
                return View(myimg);
            }
        }
        //Eidt images
        [HttpPost]
        public ActionResult EditImages(HttpPostedFileBase file, int id,  FormCollection form)
        {
            string getImgtitle = form["imgtitle"].ToString();
            string getImgdesc = form["imgdesc"].ToString();
            string getImgfullurl = form["imgfullurl"].ToString();
            string getImgName = form["imgurl_lg"].ToString();
            String getfld = form["imgfolder"].ToString();
            if (ModelState.IsValid)
            {
                

                if (file != null)// here am first checking to see if there is an uploaded file or not
                    if (file.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 3; //3 MB
                        //int MaxContentLength = 250;
                        string[] AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".gif", ".png", ".pdf" };
                        if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                        {
                            ModelState.AddModelError("File", "الملفات التالية مسموح برفعها فقط: " + string.Join(", ", AllowedFileExtensions));
                        }
                        else if (file.ContentLength > MaxContentLength)
                        {
                            ModelState.AddModelError("File", "الملف الذي تقوم برفعه كبير الحجم المسموح برفعه هو:  " + MaxContentLength + " MB");
                        }
                        else // if the file is valid in terms of size and type then we will start to upload.
                        {
                            string getw = "600"; // these are the preconfigured width and height;
                            string geth = "400"; 

                          
                            var job = new ImageJob(file, "~/UploadImages/" + getfld + "/<guid>", new Instructions("mode=max;format=jpg;width=" + getw + ";height=" + geth + ";"));
                            job.CreateParentDirectory = true;
                            job.AddFileExtension = true;
                            job.Build();
                            string fileName = Path.GetFileName(job.FinalPath);
                           getImgfullurl  = Url.Content("~/UploadImages/" + getfld + "/" + fileName); // get the total or full path of the image
                           getImgName=fileName;
                            string extension = Path.GetExtension(fileName);
                        } 
                    }
                        using (salalahDBEntities db = new salalahDBEntities())
                        {
                            //tbl_images1 tbl_image = new tbl_images1();
                            var tbl_image = (from i in db.tbl_images1
                                             where i.imgid == id
                                             select i).FirstOrDefault();
                            if (string.IsNullOrEmpty(getImgtitle))
                            {
                                tbl_image.imgtitle = "لا يوجد عنوان";
                                ViewBag.FileName = "لا يوجد عنوان";
                            }
                            else
                            {
                                tbl_image.imgtitle = getImgtitle;
                            }
                            if (string.IsNullOrEmpty(getImgdesc))
                            {
                                tbl_image.imgdesc = "لا يوجد وصف للصورة";
                                ViewBag.Desc = "لا يوجد وصف للصورة";
                            }
                            else
                            {
                                tbl_image.imgdesc = getImgdesc;
                            }
                            tbl_image.imgfullurl = getImgfullurl;
                            tbl_image.imgurl_lg = getImgName;
                            tbl_image.imgdate = DateTime.Now;
                            tbl_image.imglink = form["imglink"].ToString();
                            db.SaveChanges();
                        }
                        ModelState.Clear();
                        ViewBag.Message = "تم عمل التعديلات بنجاح";
                  
            }
            return RedirectToAction("DisplayImages");
            //return RedirectToAction("dsImages", new { imgFolder = getfld });
        }
        //GET
        // here we are going to display all the sub folders from the image folder
        [Route("DisplayImages")]
        public ActionResult DisplayImages()
        {
            //here am going to list all the sub-foldders in the image foldder

            string folderPath = Server.MapPath("~/UploadImages/");
            List<string> picFolders = new List<string>();
            DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/UploadImages/"));
            DirectoryInfo[] directories = directory.GetDirectories();

            // your code check
            foreach (DirectoryInfo folder in directories)
            {
                picFolders.Add(folder.Name);
            }
           
            ViewBag.imgFolders = picFolders.ToList();
            return View();
        }
        //Post
        //Display images
        [Route("dsImages")]
        public ActionResult dsImages(string imgfolder)
        {
            using (db)
            {
                IEnumerable<tbl_images1> disImg = db.tbl_images1.Where(x => x.imgfolder == imgfolder);
                return View("_displayImages", disImg.ToList());
            }
        }
        //Delete Images
        //Post
        public ActionResult DeleteImages(string imgurl, string imgfolder)
        {
            using (db)
            {
                if (imgurl != null)
                {
                    int imgcount = db.tbl_images1.Where(x => x.imgfullurl == imgurl).Count();

                    if (imgcount > 0)
                    {
                        var delimg = db.tbl_images1.Where(x => x.imgfullurl == imgurl).FirstOrDefault();
                        db.tbl_images1.Remove(delimg);
                        db.SaveChanges();
                        string folderPath = Server.MapPath(imgurl);
                        if (System.IO.File.Exists(folderPath))
                        {
                            System.IO.File.Delete(folderPath);
                        }
                    }
                }
                IEnumerable<tbl_images1> disImg = db.tbl_images1.Where(x => x.imgfolder == imgfolder);
                return View("_displayImages", disImg.ToList());
            }
        }
    }
}