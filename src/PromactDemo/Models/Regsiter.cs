﻿using System.ComponentModel.DataAnnotations;

namespace PromactDemo.Models
{
    public class Regsiter
    {
        [Required, Display(Name = "User Name"), MinLength(5), MaxLength(100)]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
