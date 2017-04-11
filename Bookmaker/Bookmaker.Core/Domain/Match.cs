using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Match
    {        
        public int Id { get; protected set; }        
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
            SetHostTeam(hostTeam);
            SetGuestTeam(guestTeam);
            SetStadium(stadium);
            SetStartTime(startTime);            
        }

        public void SetId(int id)
        {
            if (id < 0)
            {
                throw new Exception($"Match: Id cannot be set to '{ id }' (less than zero).");
            }

            Id = id;
        }

        public void SetHostTeam(Team team)
        {
            if (team == null)
                throw new Exception("Match: provided host team is not valid.");

            if (HostTeam == team)
                return;

            HostTeam = team;
        }

        public void SetGuestTeam(Team team)
        {
            if (team == null)
                throw new Exception("Match: provided guest team is not valid.");

            if (GuestTeam == team)
                return;

            GuestTeam = team;
        }

        public void SetStadium(Stadium stadium)
        {
            if (stadium == null)
                throw new Exception("Match: provided stadium is not valid.");

            if (Stadium == stadium)
                return;

            Stadium = stadium;
        }

        public void SetResult(Result result)
        {
            if (result == null)
                throw new Exception("Match: provided result is not valid.");

            if (Result == result)
                return;

            Result = result;            
        }

        public void SetStartTime(DateTime time)
        {
            if (time == null)
                throw new Exception("Match: provided start time is not valid.");

            if (StartTime == time)
                return;

            StartTime = time;
        }
    }
}
