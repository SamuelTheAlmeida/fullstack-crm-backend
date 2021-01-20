﻿using System.ComponentModel.DataAnnotations;

namespace FullStackCRM.Application.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
