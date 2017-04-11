using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Result
    {        
        public Guid Id { get; protected set; }        
        public Score HostScore { get; protected set; }        
        public Score GuestScore { get; protected set; }
    }
}
