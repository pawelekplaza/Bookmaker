using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; } = "super_secret_key_123456!";
        public string Issuer { get; set; } = "http://localhost:5000";
        public int ExpiryMinutes { get; set; } = 5;
    }
}
