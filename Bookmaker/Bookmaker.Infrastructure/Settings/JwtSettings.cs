using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.Settings
{
    public class JwtSettings
    {
        private string _key;
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private string _issuer;
        public string Issuer
        {
            get { return _issuer; }
            set { _issuer = value; }
        }

        private int _expiryMinutes;    
        public int ExpiryMinutes
        {
            get { return _expiryMinutes; }
            set { _expiryMinutes = value; }
        }

        public JwtSettings(string key, string issuer, int expiryMinutes)
        {
            Key = key;
            Issuer = issuer;
            ExpiryMinutes = expiryMinutes;
        }
    }
}
