using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.DTO
{
    public class TeamUpdateDto
    {
        public int Id { get; set; }
        public int? StadiumId { get; set; }
        public string Name { get; set; }
    }
}
