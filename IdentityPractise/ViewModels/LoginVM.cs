﻿using System.ComponentModel.DataAnnotations;

namespace IdentityPractise.ViewModels
{
    public class LoginVM
    {

        [Required, StringLength(50),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool RememberMe { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
