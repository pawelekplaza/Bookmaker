using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.DTO
{
    public class CityUpdateDto
    {
        public int? Id { get; set; }
        public int? CountryId { get; set; }
        public string Name { get; set; }
    }
}
