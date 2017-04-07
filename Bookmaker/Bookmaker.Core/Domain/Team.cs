using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Team
    {
        public Guid Id { get; protected set; }
        public Guid StadiumId { get; protected set; }
        public string Name { get; protected set; }        
        public IEnumerable<Match> PlayedMatches { get; protected set; }
        public IEnumerable<Match> ScheduledMatches { get; protected set; }
    }
}
