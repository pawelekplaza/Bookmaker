using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.ServicesInterfaces
{
    public interface IEncrypter
    {
        string GetSalt(string value);
        string GetHash(string value, string salt);
    }
}
