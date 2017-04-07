using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Match
    {
        public Guid Id { get; protected set; }
        public Guid HostTeamId { get; protected set; }
        public Guid GuestTeamId { get; protected set; }
        public Guid StadiumId { get; protected set; }
        public DateTime StartTime { get; protected set; }
        public Result Result { get; protected set; }

        protected Match()
        {

        }

        public Match(Guid hostTeamId, Guid guestTeamId, Guid stadiumId, DateTime startTime)
        {

        }
    }
}
