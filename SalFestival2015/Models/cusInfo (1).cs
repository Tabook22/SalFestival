//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalFestival2015.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class cusInfo
    {
        public cusInfo()
        {
            this.locs = new HashSet<loc>();
        }
    
        public int Id { get; set; }
        public string cus_title { get; set; }
        public string cus_address { get; set; }
        public string cus_office_tel { get; set; }
        public string cus_home_tel { get; set; }
        public string cus_mobile { get; set; }
        public string cus_email { get; set; }
        public string cus_img { get; set; }
        public string cus_business_type { get; set; }
        public string cus_notice { get; set; }
    
        public virtual ICollection<loc> locs { get; set; }
    }
}
