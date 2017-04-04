using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Bet
    {
        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public uint Price { get; protected set; } 
        public Match Match { get; protected set; }
        public Team Team { get; protected set; }
        public Score Score { get; protected set; }
    }
}
