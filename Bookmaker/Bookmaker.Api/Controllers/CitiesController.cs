using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using Bookmaker.Infrastructure.DTO;
using Bookmaker.Core.Utils;
using Bookmaker.Infrastructure.ServicesInterfaces;

namespace Bookmaker.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Cities")]
    public class CitiesController : Controller
    {
        private readonly ICityService _cityService;
        private readonly ILogger _logger;

        public CitiesController(ICityService cityService, ILogger<CitiesController> logger)
        {
            _cityService = cityService;
            _logger = logger;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<IEnumerable<CityDto>> GetAllAsync()
        {
            try
            {
                return await _cityService.GetAllAsync();
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not get any city.");
                return null;
            }
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<CityDto> GetAsync(int id)
        {
            try
            {
                return await _cityService.GetAsync(id);
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not get city with id '{ id }'.");
                return null;
            }
        }

        [HttpGet("name/{name}")]
        public async Task<IEnumerable<CityDto>> GetAsync(string name)
        {
            try
            {
                return await _cityService.GetAsync(name);
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not get any city with name '{ name }'.");
                return null;
            }
        }

        [HttpGet("{id}/stadiums")]
        public async Task<IEnumerable<StadiumDto>> GetStadiumsAsync(int id)
        {
            try
            {
                return await _cityService.GetStadiumsAsync(id);
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not get any stadium for city with id '{ id }'.");
                return null;
            }
        }

        // POST: api/Cities
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CityCreateDto request)
        {
            try
            {
                await _cityService.CreateAsync(request);
                return Ok();
            }
            catch (InvalidDataException ex)
            {
                return Json(new { message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        
        // PUT: api/Cities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody]CityUpdateDto request)
        {
            try
            {
                request.Id = id;

                await _cityService.UpdateAsync(request);
                return Ok();
            }
            catch (InvalidDataException ex)
            {
                return Json(new { message = ex.Message });
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _cityService.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidDataException ex)
            {
                return Json(new { message = ex.Message });
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
