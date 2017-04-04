using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Interfaces
{
    public interface IEmailValidator
    {
        bool IsUnique(string email);
        bool IsValid(string email);        
    }
}
