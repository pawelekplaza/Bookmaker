using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Score
    {
        public ushort Goals { get; protected set; }
        public ushort Shots { get; protected set; }
    }
}
