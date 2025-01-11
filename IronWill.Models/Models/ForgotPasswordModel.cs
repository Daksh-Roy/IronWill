using System;
using System.ComponentModel.DataAnnotations;

namespace IronWill.Models.Models
{
	public class ForgotPasswordModel
	{
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

