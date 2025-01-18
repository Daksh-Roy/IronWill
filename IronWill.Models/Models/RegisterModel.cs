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
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 8)]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        [Phone]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string Address { get; set; } = string.Empty;

        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        public UserStatus Status { get; set; } = UserStatus.Active;
    }
}

