using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Result
    {
        public Score HostScore { get; protected set; }
        public Score GuestScore { get; protected set; }
    }
}
