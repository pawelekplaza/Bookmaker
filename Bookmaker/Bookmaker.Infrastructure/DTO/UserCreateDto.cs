using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookmaker.Infrastructure.DTO
{
    public class UserCreateDto
    {        
        [EmailAddress]
        public string Email { get; set; }
        
        public string Username { get; set; }        
        public string Password { get; set; }
    }
}
