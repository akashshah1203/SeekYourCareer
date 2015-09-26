using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekYourCareer.Models
{
    public class AddNewRepresentative
    {
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[0-9])[A-Za-z0-9]+$", ErrorMessage = "Only alphabets and numbers allowed. Must contain atleast 1 alphabet and 1 number.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Minimum 6 and atmost 20 characters allowed.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Minimum 6 and atmost 20 characters allowed.")]
        [Compare("Password", ErrorMessage = "Password not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        [StringLength(30, ErrorMessage = "Atmost 30 characters allowed.")]
        public string companyName { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(200, ErrorMessage = "Atmost 200 characters allowed.")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Only 10 characters allowed.")]
        public string phoneNumber { get; set; }

        [Required]
        [Display(Name = "Email ID")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string EmailId { get; set; }

        [Required]
        [Display(Name = "Training ")]
        public bool Training { get; set; }

        [Required]
        [Display(Name = "Workshop ")]
        public bool Workshop { get; set; }

        [Required]
        [Display(Name = "Job ")]
        public bool Job { get; set; }
        
        [Required]
        [Display(Name = "Internship ")]
        public bool Internship { get; set; }
    }
}