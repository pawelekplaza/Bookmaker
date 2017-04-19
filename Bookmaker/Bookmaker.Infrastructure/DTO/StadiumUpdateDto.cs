using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.DTO
{
    public class StadiumUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
    }
}
