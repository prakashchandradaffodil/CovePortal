using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Cove.Web.Models
{
    public class RegisterModel
    {
        [Key]
        public string UserId { get; set; }

        public string Role { get; set; }

        [Required(ErrorMessage = "Please Enter FirstName")]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "The FirstName length should have  min 2 and max 50 characters. ", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter LastName")]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "The LastName length should have  min 2 and max 50 characters.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email")]
        [Display(Name = "Email Address")]
        [Remote("EmailAlreadyExists", "Account",HttpMethod ="Get", ErrorMessage = "Email already exists")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Invalid password format")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter DateOfBirth")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [StringLength(10, ErrorMessage = "The PhoneNumber length max lenth should be 10")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone Number")]
        [Remote("PhoneAlreadyExists", "Account", HttpMethod = "Get", ErrorMessage = "Phone already exists")]
        public string Phone { get; set; }

        //[RegularExpression(@"^[a-zA-Z0-9_]*$", ErrorMessage = "Space not allowed")] 
        [StringLength(100, ErrorMessage = "The Address value cannot exceed 100 characters. ")]
        public string Address { get; set; }

        [Display(Name = "Specialisation")]
        public string Specialisations { get; set; }
        public IEnumerable<SelectListItem> SpecialisationsListDDL { get; set; }

        [Display(Name = "Upload Files(Sample Work, CV)")]
        public string FilePath { get; set; }

        [Display(Name = "Add Links")]
        public string Links { get; set; }
        public string Profile { get; set; }
        public string AssetIds { get; set; }

    }
}
