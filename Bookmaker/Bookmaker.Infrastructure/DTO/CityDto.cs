﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.DTO
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
