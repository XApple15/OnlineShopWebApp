﻿using System.ComponentModel.DataAnnotations;

namespace OnlineShop.API.Models.DTO
{
    public class RegisterRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
