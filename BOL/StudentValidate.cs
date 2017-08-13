using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace BOL
{

    public class StudentValidate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Remote("IsEmailAvailble", "Register", ErrorMessage = "Email Already Exist.")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(512,ErrorMessage = "Password must not less than 6 character", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
//        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfrimPassword { get; set; }

        [Required]
        public string Batch { get; set; }
        [Required]
        [Display(Name = "Room No.")]
        public string RoomNo { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone")]
        
        [Range(typeof(int), "6", "15", ErrorMessage = "Phone Number not valid ,it  must be  6 to 15 number")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; }

//        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


//        [Required]
        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        public string NewConfrimPassword { get; set; }
    }
    [MetadataType(typeof(StudentValidate))]
    public partial class Student
    {
        public string ConfrimPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewConfrimPassword { get; set; }
    }
}
