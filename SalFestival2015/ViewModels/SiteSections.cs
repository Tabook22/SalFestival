using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalFestival2015.ViewModels
{
    public class SiteSections
    {
        public int secId { get; set; }

        [Required(ErrorMessage = "الرجاء كتابة عنوان القسم")]
        [Display(Name = "عنوان القسم")]
        public string sec_title { get; set; }

         [Display(Name = "موقع القسم")]
        public MySiteSections sec_loc { get; set; }

        [Display(Name = "وصف مختصر")]
        public string sec_desc { get; set; }

        [Display(Name = "الحالة")]
        public Nullable<int> sec_status { get; set; }
    }

    public enum MySiteSections
    {
        [Display(Name = "site section 1")]
        Section_1,

        [Display(Name = "site section 2")]
        Section_2,

        [Display(Name = "site section 3")]
        Section_3,

        [Display(Name = "site section 4")]
        Section_4
    }
}