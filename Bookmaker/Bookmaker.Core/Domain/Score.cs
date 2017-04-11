using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Score
    {        
        public int Goals { get; protected set; }
        public int Shots { get; protected set; }
    }
}
