using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.DTO
{
    public class MatchUpdateDto
    {
        public int Id { get; set; }
        public int? HostTeamId { get; set; }
        public int? GuestTeamId { get; set; }
        public int? StadiumId { get; set; }
        public DateTime? StartTime { get; set; }
        public int? ResultId { get; set; }
    }
}
