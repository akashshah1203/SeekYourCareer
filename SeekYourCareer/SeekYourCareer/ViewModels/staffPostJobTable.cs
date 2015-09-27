using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekYourCareer.ViewModels
{
    public class staffPostJobTable
    {
        public string jobType { get; set; }
        public decimal MinSSCPercent { get; set; }
        public decimal MinHSCPercent { get; set; }
        public decimal MinGradAvg { get; set; }
        public decimal MinPostAvg { get; set; }
        public int SalPerMonth { get; set; }
        public int Experience { get; set; }
        public int LocationID { get; set; }
        public DateTime LastDate { get; set; }
        public string jobID { get; set; }
    }
}