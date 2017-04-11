using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Wallet
    {        
        public Guid Id { get; protected set; }
        public User User { get; protected set; }       
        public int Points { get; protected set; }        
    }
}
