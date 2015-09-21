using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjSeekCareer.Models
{
    public class JobPrerequisite
    {
        public double MinSSCPercent { get; set; }
        public double MinHSCPercent { get; set; }
        public double MinGradAvg { get; set; }
        public double MinPGAvg { get; set; }
        public int Experience { get; set; }
    }
}