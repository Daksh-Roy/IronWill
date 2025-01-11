using System;
using System.ComponentModel.DataAnnotations;
using IronWill.Models.Enums;

namespace IronWill.Models.Models
{
	public class RegisterModel
	{
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 8)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(15)]
        [Phone]
        public string MobileNumber { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender Gender { get; set; }
    }
}

