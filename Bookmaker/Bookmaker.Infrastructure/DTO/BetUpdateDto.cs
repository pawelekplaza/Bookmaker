using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.DTO
{
    public class BetUpdateDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? MatchId { get; set; }
        public int? TeamId { get; set; }
        public int? Price { get; set; }
        public int? ScoreId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
