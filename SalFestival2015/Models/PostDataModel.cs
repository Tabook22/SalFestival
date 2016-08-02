using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalFestival2015.Models
{
    public class PostDataModel
    {
        public List<tbl_posts> mPost { get; set; }
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
        public int NoOfPages { get; set; }
        public DateTime postDate { get; set; }
    }
}
