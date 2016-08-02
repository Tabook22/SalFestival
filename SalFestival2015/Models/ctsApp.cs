using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalFestival2015.Models
{
    public class ctsApp
    {
        public int Id { get; set; }
        public DateTime Cdate { get; set; }
        public string Name{ get; set; }
        public string IdNo  { get; set; }
        public string State  { get; set; }
        public string Tel { get; set; }
        public string Contest { get; set; }
    }
    public class lstCstUsers
    {

        public List<ctsApp> cstusers { get; set; }
    }
}