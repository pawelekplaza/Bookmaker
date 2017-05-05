using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Bookmaker.Infrastructure.DTO;
using Microsoft.Extensions.Logging;
using Bookmaker.Core.Utils;
using Bookmaker.Infrastructure.ServicesInterfaces;

namespace Bookmaker.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Stadiums")]
    public class StadiumsController : Controller
    {
        private readonly IStadiumService _stadiumService;
        private readonly ILogger _logger;

        public StadiumsController(IStadiumService stadiumService, ILogger<StadiumsController> logger)
        {
            _stadiumService = stadiumService;
            _logger = logger;
        }

        // GET: api/Stadiums
        [HttpGet]
        public async Task<IEnumerable<StadiumDto>> GetAllAsync()
        {
            try
            {
                return await _stadiumService.GetAllAsync();
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not get any stadium.");
                return null;
            }
        }

        // GET: api/Stadiums/5
        [HttpGet("{id}")]
        public async Task<StadiumDto> GetAsync(int id)
        {
            try
            {                
                return await _stadiumService.GetAsync(id);
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not get stadium with id '{ id }'.");
                return null;
            }
        }

        // GET: api/Stadiums/5/matches
        [HttpGet("{id}/matches")]
        public async Task<IEnumerable<MatchDto>> GetMatchesAsync(int id)
        {
            try
            {
                return await _stadiumService.GetMatchesAsync(id);
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not get any match for stadium with id '{ id }'.");
                return null;
            }
        }
        
        // POST: api/Stadiums
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]StadiumDto request)
        {
            try
            {
                await _stadiumService.CreateAsync(request);
                return Ok();
            }
            catch (InvalidDataException ex)
            {
                _logger.LogInformation("Could not add new stadium - invalid data.");
                return Json(new { message = ex.Message });
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not add new stadium.");
                return BadRequest();
            }
        }
        
        // PUT: api/Stadiums/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody]StadiumUpdateDto request)
        {
            try
            {
                request.Id = id;

                await _stadiumService.UpdateAsync(request);
                return Ok();
            }
            catch (InvalidDataException ex)
            {
                _logger.LogInformation("Could not update stadium - invalid data.");
                return Json(new { message = ex.Message });
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not update stadium.");
                return BadRequest();
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _stadiumService.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidDataException ex)
            {
                _logger.LogInformation($"Could not delete stadium with id '{ id }' - invalid data.");
                return Json(new { message = ex.Message });
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not delete stadium with id '{ id }'.");
                return BadRequest();
            }
        }
    }
}
