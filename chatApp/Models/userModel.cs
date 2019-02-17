using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace chatApp.Models
{
    public class userModel
    {
        public int userid { get; set; }
        [Required(ErrorMessage = "Email id Is required")]
        public string email { get; set; }
        [Required(ErrorMessage = "Mobile Number Is required")]
        public string mobile { get; set; }
        [Required(ErrorMessage = "Password Is required")]
        public string password { get; set; }
        [Required(ErrorMessage = "Confirm Password Is required")]
        [Compare("password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string confirmpassword { get; set; }
        [Required(ErrorMessage = "Date Of Birth Is required")]
        public string dob { get; set; }
    }
}