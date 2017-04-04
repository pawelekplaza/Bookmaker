using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Interfaces
{
    public interface IEmailChecker
    {
        bool IsValid(string email);
    }
}
