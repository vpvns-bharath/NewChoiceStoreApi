﻿using System.ComponentModel.DataAnnotations;

namespace NewChoiceStoreAPI.Models
{
    public class UserDetails
    {
        [Key]
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
    }
}
