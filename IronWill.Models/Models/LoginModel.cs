﻿using System;
using System.ComponentModel.DataAnnotations;

namespace IronWill.Models.Models
{
	public class LoginModel
	{
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
