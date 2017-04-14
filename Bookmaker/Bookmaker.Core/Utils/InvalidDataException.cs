using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Utils
{
    public class InvalidDataException : Exception
    {        
        public InvalidDataException(string message) : base(message)
        {
        }
    }
}
