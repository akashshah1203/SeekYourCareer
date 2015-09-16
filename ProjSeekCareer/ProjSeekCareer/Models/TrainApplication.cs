﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjSeekCareer.Models
{
    public class TrainApplication
    {
        public string ApplicantId { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string TrainingId { get; set; }
        public DateTime AppDate { get; set; }
        public string CorrAddress { get; set; }
        public string ContactNo { get; set; }
        public string SelectionStatus { get; set; }
     }
}