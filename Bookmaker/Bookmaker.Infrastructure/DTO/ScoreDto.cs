﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.DTO
{
    public class ScoreDto
    {
        public int Id { get; set; }
        public int? Shots { get; set; }
        public int? Goals { get; set; }
    }
}
