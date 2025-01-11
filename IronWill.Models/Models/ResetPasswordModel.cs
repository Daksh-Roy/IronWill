using System;
using System.ComponentModel.DataAnnotations;

namespace IronWill.Models.Models
{
    public class ResetPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 8)]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 8)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Token { get; set; }
    }
}

