using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bookmaker.Infrastructure.DTO;
using Bookmaker.Infrastructure.ServicesInterfaces;
using Microsoft.Extensions.Logging;
using Bookmaker.Core.Utils;

namespace Bookmaker.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Matches")]
    public class MatchesController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly ILogger _logger;

        public MatchesController(IMatchService matchService, ILogger<MatchesController> logger)
        {
            _matchService = matchService;
            _logger = logger;
        }

        // GET: api/Matches
        [HttpGet]
        public async Task<IEnumerable<MatchDto>> GetAllAsync()
        {
            try
            {

                return await _matchService.GetAllAsync();
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not get any match.");
                return null;
            }
        }

        // GET: api/Matches/5
        [HttpGet("{id}")]
        public async Task<MatchDto> GetAsync(int id)
        {
            try
            {
                return await _matchService.GetByIdAsync(id);
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not get match with id '{ id }'.");
                return null;
            }
        }
        
        // POST: api/Matches
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]MatchDto request)
        {
            try
            {
                await _matchService.CreateAsync(request);
                return Ok();
            }
            catch (InvalidDataException ex)
            {
                _logger.LogInformation("Could not create a new match.");
                return Json(new { message = ex.Message });
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not create a new match.");
                return BadRequest();
            }
        }
        
        // PUT: api/Matches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody]MatchUpdateDto request)
        {
            try
            {
                request.Id = id;

                await _matchService.UpdateAsync(request);
                return Ok();
            }
            catch (InvalidDataException ex)
            {
                _logger.LogInformation($"Could not update match with id '{ id }'.");
                return Json(new { message = ex.Message });
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not update match with id '{ id }'.");
                return BadRequest();
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _matchService.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidDataException ex)
            {
                _logger.LogInformation($"Could not delete match with id '{ id }'.");
                return Json(new { message = ex.Message });
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not delete match with id '{ id }'.");
                return BadRequest();
            }
        }
    }
}
