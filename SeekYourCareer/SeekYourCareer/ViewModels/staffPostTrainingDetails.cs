using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekYourCareer.ViewModels
{
    public class staffPostTrainingDetails
    {
        public string location { get; set; }
        public string domain { get; set; }
        public char graduation { get; set; }
        public char postgrad { get; set; }
        public int experience { get; set; }
        public DateTime startDate { get; set; }
        public int duration { get; set; }
        public int seats { get; set; }
        public string description { get; set; }
    }
}