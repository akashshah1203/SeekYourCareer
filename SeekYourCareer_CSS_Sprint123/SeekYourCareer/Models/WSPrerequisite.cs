using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class WSPrerequisite
    {
        public int WorkshopId { get; set; }
        public double MinGradPct { get; set; }
        public double MinPGPct { get; set; }
        public int MinExperience { get; set; }
    }
}