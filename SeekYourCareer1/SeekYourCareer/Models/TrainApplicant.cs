using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class TrainApplicant
    {
        public int ApplicantId { get; set; }
        public string Name { get; set; }
        public string AppDate { get; set; }
        public double SSCPercent { get; set; }
        public double HSCPercent { get; set; }
        public double GradPercent { get; set; }
        public double PGPercent { get; set; }
        public int Experience { get; set; }
    }
}