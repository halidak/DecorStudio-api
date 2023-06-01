﻿using System.ComponentModel.DataAnnotations;

namespace DecorStudio_api.DTOs
{
    public class UserLoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required] 
        public string Password { get; set; }
    }
}
