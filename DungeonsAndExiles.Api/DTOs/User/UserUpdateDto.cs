﻿using DungeonsAndExiles.Api.Models.Domain;

namespace DungeonsAndExiles.Api.DTOs.User
{
    public class UserUpdateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
