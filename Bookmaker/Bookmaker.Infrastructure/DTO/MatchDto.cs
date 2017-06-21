using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.DTO
{
    public class MatchDto
    {
        public int Id { get; set; }
        public int HostTeamId { get; set; }
        public string HostTeamName { get; set; }
        public int GuestTeamId { get; set; }
        public string GuestTeamName { get; set; }
        public int StadiumId { get; set; }
        public DateTime StartTime { get; set; }
        public int? ResultId { get; set; }
    }
}