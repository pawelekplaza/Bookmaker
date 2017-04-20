using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.DTO
{
    public class ResultUpdateDto
    {
        public int Id { get; set; }
        public int? HostScoreId { get; set; }
        public int? GuestScoreId { get; set; }
    }
}
