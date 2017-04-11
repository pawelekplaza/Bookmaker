using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookmaker.Infrastructure.DTO
{
    public class UserUpdateDto
    {
        [MinLength(4)]
        [MaxLength(50)]
        public string Username { get; set; }

        [MinLength(6)]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
