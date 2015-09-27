using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekYourCareer.Models
{
    public class PasswordChange
    {
        [Required]
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[0-9])[A-Za-z0-9]+$", ErrorMessage = "Only alphabets and numbers allowed. Must contain atleast 1 alphabet and 1 number.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Minimum 6 and atmost 20 characters allowed.")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[0-9])[A-Za-z0-9]+$", ErrorMessage = "Only alphabets and numbers allowed. Must contain atleast 1 alphabet and 1 number.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Minimum 6 and atmost 20 characters allowed.")]
        public string NewPassword { get; set; }


        [Required]
        [Display(Name = "Re-enter New Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password not match")]
        public string ReConfirmPassword { get; set; }

    }
}