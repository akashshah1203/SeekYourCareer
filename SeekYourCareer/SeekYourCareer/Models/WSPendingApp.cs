using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class WSPendingApp
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public double GradPercent { get; set; }
        public double PGPercent { get; set; }
        public int WorkExpYears { get; set; }
    }
}