﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace QueuePulse.Models.Entities
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string Name { get; set; }
    }
}
