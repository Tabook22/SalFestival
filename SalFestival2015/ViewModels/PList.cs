using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalFestival2015.ViewModels
{
    public class PList
    {
        public int Id { get; set; }

        [Display(Name="عنوان المقالة")]
        public string post_title { get; set; }
             [Display(Name = "تاريخ المقالة")]
        public Nullable<System.DateTime> post_date { get; set; }
             [Display(Name = "حالة المقالة")]
        public Nullable<int> post_status { get; set; }        
    }
}
