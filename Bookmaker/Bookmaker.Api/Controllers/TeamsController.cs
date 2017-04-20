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
    [Route("api/Teams")]
    public class TeamsController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly ILogger _logger;

        public TeamsController(ITeamService teamService, ILogger<TeamsController> logger)
        {
            _teamService = teamService;
            _logger = logger;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<IEnumerable<TeamDto>> GetAllAsync()
        {
            try
            {
                return await _teamService.GetAllAsync();
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not get any team.");
                return null;
            }
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<TeamDto> GetAsync(int id)
        {
            try
            {
                return await _teamService.GetByIdAsync(id);
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not get team with id '{ id }'.");
                return null;
            }
        }
        
        // POST: api/Teams
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]TeamDto request)
        {
            try
            {
                await _teamService.CreateAsync(request);
                return Ok();
            }
            catch (InvalidDataException ex)
            {
                _logger.LogInformation($"Could not create new team.");
                return Json(new { message = ex.Message });
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not create new team.");
                return BadRequest();
            }
        }
        
        // PUT: api/Teams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody]TeamUpdateDto request)
        {
            try
            {
                request.Id = id;

                await _teamService.UpdateAsync(request);
                return Ok();
            }
            catch (InvalidDataException ex)
            {
                _logger.LogInformation($"Could not update team with id '{ id }'.");
                return Json(new { message = ex.Message });
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not update team with id '{ id }'.");
                return BadRequest();
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _teamService.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidDataException ex)
            {
                _logger.LogInformation($"Could not delete team with id '{ id }'.");
                return Json(new { message = ex.Message });
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not delete team with id '{ id }'.");
                return BadRequest();
            }
        }
    }
}
