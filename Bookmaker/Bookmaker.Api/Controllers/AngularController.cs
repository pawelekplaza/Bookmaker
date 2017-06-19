using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookmaker.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Angular")]
    public class AngularController : Controller
    {
        // GET: api/Angular
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return await Task.Factory.StartNew(() =>
                Json(new[]
                {
                    new {
                        productId = 2,
                        productName = "Garden cart",
                        productCode = "GDN-0023",
                        releaseDate = "March 18, 2017",
                        description = "15 gallon capacity rolling garden cart",
                        price = 32.99,
                        starRating = 4.2,
                        imageUrl = "https://i5.walmartimages.com/asr/9b3caed0-0793-429a-a318-36eee14f5ca4_1.e4e12a09d441f3378b4c294302a16fbf.jpeg"
                    },
                    new
                    {
                        productId =  5,
                        productName =  "Hammer",
                        productCode = "BCE-14",
                        releaseDate = "May 21, 2017",
                        description = "Curved claw steel hammer",
                        price = 8.99,
                        starRating = 4.8,
                        imageUrl = "https://images-na.ssl-images-amazon.com/images/I/31eSHK9kHFL._SL500_AC_SS350_.jpg"
                    },
                    new
                    {
                        productId =  3,
                        productName =  "Metal nail",
                        productCode = "MNX-663",
                        releaseDate = "June 11, 2017",
                        description = "A pin-shaped object of metal which is used as a fastener, as a peg to hang something, or sometimes as a decoration.",
                        price = 1.99,
                        starRating = 4.9,
                        imageUrl = "http://pngimg.com/uploads/metal_nail/metal_nail_PNG16997.png"
                    },
                    new
                    {
                        productId =  6,
                        productName =  "Bosch angle grinder",
                        productCode = "PWR-253",
                        releaseDate = "June 4, 2017",
                        description = "The Bosch 1375A 4-1/2 In. Angle Grinder features a powerful 6.0-amp motor that produces 11,000 no-load rpm, making this compact grinder a powerhouse tool.",
                        price = 49.99,
                        starRating = 4.7,
                        imageUrl = "https://www.boschtools.com/us/en/ocsmedia/optimized/full/GWS_1350VS_Hero.png"
                    }
                }));
        }

        //// GET: api/Angular/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Angular
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Angular/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
