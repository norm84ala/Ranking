using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ranking
{
    public class WebHits
    {
        public string website { get; set; }
        public int TotalVisit { get; set; }
        //public DateTime ReportedDate { get; set; }

    }

    public class WebLog
    {
        public int TotalVisit { get; set; } 
        public string ReportedDate { get; set; }

    }
}