using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class RepresentativeAddWorkshopOffer
    {
        [Display(Name = "Comapny")]
        public string Company { get; set; }

        [Display(Name = "Domain")]
        public string domain { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "From Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime fromDate { get; set; }

        [Required]
        [Display(Name = "To Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime toDate { get; set; }

        [Required]
        [Display(Name = "No. Of Seats")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Only between 0-100.")]
        public int noOfSeats { get; set; }

        [Required]
        [Display(Name = "Minimum Graduation Perecentage ")]
        [Range(0.0, 100.0, ErrorMessage = "Only between 0-100.")]
        public float GraduationPercentage { get; set; }

        [Required]
        [Display(Name = "Minimum Post Graduation Percentage  ")]
        [Range(0.0, 100.0, ErrorMessage = "Only between 0-100.")]
        public float PostGraduationPercentage { get; set; }

        [Required]
        [Display(Name = "Minimum Experience")]
        public int Experience { get; set; }

        [Display(Name = "Description")]
        [StringLength(1024, MinimumLength = 0, ErrorMessage = "Maximum 1024 characters allowed.")]
        public string description { get; set; }
    }
}