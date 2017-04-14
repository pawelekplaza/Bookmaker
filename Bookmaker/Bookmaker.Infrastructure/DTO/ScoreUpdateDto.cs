using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.DTO
{
    public class ScoreUpdateDto
    {
        public int Id { get; set; }
        public int? Goals { get; set; }
        public int? Shots { get; set; }
    }
}
