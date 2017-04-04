using Bookmaker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Utils
{
    public class EmailChecker : IEmailChecker
    {
        public static bool IsValid(string email)
        {
            //TODO
            if (email.Contains("@"))
                return true;
            else
                return false;
        }
    }
}
