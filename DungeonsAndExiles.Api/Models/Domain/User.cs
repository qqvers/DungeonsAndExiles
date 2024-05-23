﻿using System.ComponentModel.DataAnnotations;

namespace DungeonsAndExiles.Api.Models.Domain
{
    public class User : Character
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }

        public Role Role { get; set; }
        public List<Player> Players { get; set; }
    }
}
