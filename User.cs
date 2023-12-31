﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibaryManagmentSystemAPI.Models
{
    public class User
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        [JsonIgnore]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [JsonIgnore]
        public string Phone { get; set; }

    }
}