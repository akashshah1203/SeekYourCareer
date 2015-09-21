using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjSeekCareer.Models
{
    public class WSPrerequisite
    {
        public string WorkshopId { get; set; }
        public double MinGradPct { get; set; }
        public double MinPGPct { get; set; }
        public int MinExperience { get; set; }
    }
}