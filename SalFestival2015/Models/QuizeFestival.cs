using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalFestival2015.Models
{
    public class QuizeFestival
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
        public string  qans { get; set; }
        public string qno { get; set; }
    }

    public class lstQzUsers
    {

        public List<QuizeFestival> cstusers { get; set; }
    }
}