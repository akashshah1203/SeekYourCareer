using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjSeekCareer.Models
{
    public class Workshop
    {
        public string WorkshopId { get; set; }
        public int RepId { get; set; }
        public string Domain { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int NoOfSeats { get; set; }
        public float MinGradPct { get; set; }
        public float MinPGPct { get; set; }
        public string WorkshopDesc { get; set; }
        public string StaffApproval { get; set; }
    }
}