using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeekYourCareer.Models
{
    public class Applicant
    {
        [Required]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Minimum 8 and atmost 15 characters allowed.")]
        [RegularExpression(@"^[a-zA-Z0-9_]*$", ErrorMessage = "Only alphabets, numbers and underscore allowed.")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Minimum 8 and atmost 15 characters allowed.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Minimum 8 and atmost 15 characters allowed.")]
        public string ConfirmPassword { get; set; }
        
        [Required]
        [StringLength(30, ErrorMessage = "Atmost 30 characters allowed.")]
        public string Name { get; set; }
        
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Only 10 characters allowed.")]
        public string ContactNumber { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string EmailId { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "Atmost 50 characters allowed.")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        
        [Required]
        [Range(0.0,100.0)]
        public float SscPercentage { get; set; }


        [Required]
        [Range(0.0, 100.0)]
        public float HscPercentage { get; set; }

        [Required]
        public string CompletedGraduation { get; set; }


        [Required]
        [Range(0.0, 100.0)]
        public float GraduationPercentage { get; set; }

        [Required]
        public string CompletedPostGraduation { get; set; }


        [Required]
        [Range(0.0, 100.0)]
        public float PostGraduationPercentage { get; set; }

        [Required]
        public string PreviousWorkExperience { get; set; }

        [Required]
        [Range(0,Int32.MaxValue)]
        public int WorkExperience { get; set; }


    }
}