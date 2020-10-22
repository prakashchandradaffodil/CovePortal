using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cove.ClassLibrary.Model
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        //[ForeignKey("ApplicationUserIdentity")]
        public string UserId { get; set; }
        //public ApplicationUserIdentity User { get; set; }
        [Required]
        public string RoleId { get; set; }

        [Required(ErrorMessage = "Please Enter FirstName")]
        [Display(Name = "FirstName")]
        [StringLength(50, ErrorMessage = "The FirstName value cannot exceed 50 characters. ", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter LastName")]
        [Display(Name = "LastName")]
        [StringLength(50, ErrorMessage = "The LastName value cannot exceed 50 characters. ", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please Enter DateOfBirth")]
        [Display(Name = "DateOfBirth")]
        public string DateOfBirth { get; set; }

        [StringLength(10, ErrorMessage = "The Phone value cannot exceed 10 digit.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [StringLength(100, ErrorMessage = "The Address value cannot exceed 100 characters. ")]
        public string Address { get; set; }

        public string Specialisations { get; set; }
        public string Links { get; set; }
        public string Profile { get; set; }
    }
}


