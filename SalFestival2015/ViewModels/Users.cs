using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalFestival2015.ViewModels
{
    public class Users
    {
        [Display(Name = "ألإسم الأول:")]
        [Required(ErrorMessage = "الرجاء كتاب الإسم", AllowEmptyStrings = false)]
        public string fname { get; set; }

        [Required(ErrorMessage = "الرجاء كتاب الإسم", AllowEmptyStrings = false), Display(Name = "الإسم ألأخير")]
        public string lname { get; set; }

        [Required(ErrorMessage = "الرجاء كتابة البريد الإلكتروني", AllowEmptyStrings = false), Display(Name = "البريد الأكتروني")]
        public string email { get; set; }

        [Required(ErrorMessage = "الرجاء كتابة إسم المستخدم", AllowEmptyStrings = false)]
        [Display(Name = "إسم المستخدم")]
        public string un { get; set; }

        [DataType(DataType.Password)]
        [StringLength(16, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required(ErrorMessage = "الرجاء كتابة كلمة المرور", AllowEmptyStrings = false)]
        [Display(Name = "كلمة المرور")]
        public string ps { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("ps", ErrorMessage = "كلمتي المرور غير متطابقتان")]
        public string psc { get; set; }

        [Required(ErrorMessage = "الرجاء إختيار صفه المستخدم", AllowEmptyStrings = false)]
        [Display(Name = "صفه المستخدم")]
        public string role { get; set; }
    }
}