using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
    }
}
