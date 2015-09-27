using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class WorkshopApplication
    {
    //    UserId] INT NOT NULL, 
    //[WorkshopId] VARCHAR(10) NOT NULL, 
    //[AppDate] DATE NOT NULL, 
    //[LocationID] INT NOT NULL,
    //[Location] VARCHAR(30) NOT NULL, 
    //[Status] VARCHAR(10) NOT NULL,

        public int UserID { get; set; }
        public string WorkshopID { get; set; }
        public DateTime AppDate { get; set; }
        public int LocationID { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
    }
}