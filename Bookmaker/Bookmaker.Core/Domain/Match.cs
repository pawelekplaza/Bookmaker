using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        protected Match()
        {

        }

        public Match(Team hostTeam, Team guestTeam, Stadium stadium, DateTime startTime)
        {

        }
    }
}
