using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalFestival2015.ViewModels
{
    public class ImgUpload
    {

        [Display(Name="عنوان الصورة")]
        public string imgtitle { get; set; }
        [Display(Name="وصف الصورة")]
        public string imgdesc { get; set; }
        [Display(Name="رابط الصورة(Url)")]
        public string imglink { get; set; }
        [Display(Name="عرض الصورة")]
        [Range(0,int.MaxValue, ErrorMessage="الرجاء كتابة أرقام فقط")]
        public string imgw { get; set; }
        [Display(Name="إرتفاع الصورة")]
        [Range(0,int.MaxValue, ErrorMessage="الرجاء كتابة أرقام فقط")]
        public string imgh { get; set; }
    }
}