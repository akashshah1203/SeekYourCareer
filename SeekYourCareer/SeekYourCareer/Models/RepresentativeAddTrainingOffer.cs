using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class RepresentativeAddTrainingOffer
    {
        [Display(Name = "Comapny")]
        public string Company { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Domain")]
        public string domain { get; set; }



        [Required]
        [Display(Name = "Graduation")]
        public bool graduation { get; set; }

        [Required]
        [Display(Name = "Post Graduation")]
        public bool postGraduation { get; set; }

        [Required]
        [Display(Name = "Experience")]
        public int Experience { get; set; }

        [Required]
        [Display(Name = "Starting Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }

        [Required]
        [Display(Name = "Duration")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Greater Than 0.")]
        public int duration { get; set; }
        
        [Required]
        [Display(Name = "No. Of Seats")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Greater Than 0")]
        public int noOfSeats { get; set; }

        
        [Display(Name = "Description")]
        [StringLength(1024, MinimumLength = 0, ErrorMessage = "Maximum 1024 characters allowed.")]
        public string description { get; set; }

        

    }
}