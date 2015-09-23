﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class WSSelectedApp
    {
        public int ApplicantId { get; set; }
        public int WorkshopId { get; set; }
        public DateTime AppDate { get; set; }

        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
    }
}