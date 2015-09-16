using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace SeekYourCareer.Models
{
    public class LoginPage
    {
        [Required]
        [Display(Name = "Type Of User")]
        public string TypeOfUser { get; set; }


        [Required]
        [Display(Name = "User Name")]
        [RegularExpression(@"^[a-zA-Z0-9_]*$", ErrorMessage = "Only alphabets, numbers and underscore allowed.")]
        public string UserName { get; set; }


        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Minimum 8 and atmost 15 characters allowed.")]
        public string Password { get; set; }
    }
}