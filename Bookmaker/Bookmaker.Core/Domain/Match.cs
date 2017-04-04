using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Match
    {
        public Guid Id { get; protected set; }
        public Team HostTeam { get; protected set; }
        public Team GuestTeam { get; protected set; }
        public Stadium Stadium { get; protected set; }
        public DateTime StartTime { get; protected set; }
        public Result Result { get; protected set; }
    }
}
