﻿using System.ComponentModel.DataAnnotations;

namespace MaximProject.ViewModel
{
    public class LoginVM
    {

        [Required]
        public string? UserNameOrEmail { get; set; }
        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
