using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjSeekCareer.Models
{
    public class TrainApplicant
    {
        public string ApplicantId { get; set; }
        public string Name { get; set; }
        public DateTime AppDate { get; set; }
        public double SSCPercent { get; set; }
        public double HSCPercent { get; set; }
        public double GradPercent { get; set; }
        public double PGPercent { get; set; }
        public int Experience { get; set; }
    }
}