using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class Workshop
    {
        public int WorkshopId { get; set; }
        public int RepId { get; set; }
        public string Domain { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int NoOfSeats { get; set; }
        public double MinGradPct { get; set; }
        public double MinPGPct { get; set; }
        public string WorkshopDesc { get; set; }
        public string StaffApproval { get; set; }
        public string Location { get; set; }
        public int MinExperience { get; set; }
    }
}