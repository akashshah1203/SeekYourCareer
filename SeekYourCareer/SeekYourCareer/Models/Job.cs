using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class Job
    {
        public string JobId { get; set; }
        public int RepId { get; set; }
        public string StreamCode { get; set; }
        public string JobType { get; set; }
        public float MinSSCPercent { get; set; }
        public float MinHSCPercent { get; set; }
        public float MinGradAvg { get; set; }
        public float MinPGAvg { get; set; }
        public int SalPerMonth { get; set; }
        public int Experience { get; set; }
        public DateTime AppLastDate { get; set; }
        public string StaffApprovalStatus { get; set; }
        public string CorrespondanceAddress { get; set; }
     }
}