using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bookmaker.Infrastructure.DTO;
using Bookmaker.Core.Utils;
using Bookmaker.Infrastructure.ServicesInterfaces;
using Microsoft.Extensions.Logging;

namespace Bookmaker.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Bets")]
    public class BetsController : Controller
    {
        private readonly IBetService _betService;
        private readonly ILogger _logger;

        public BetsController(IBetService betService, ILogger<BetsController> logger)
        {
            _betService = betService;
            _logger = logger;
        }

        // GET: api/Bets
        [HttpGet]
        public async Task<IEnumerable<BetDto>> GetAllAsync()
        {
            try
            {
                return await _betService.GetAllAsync();
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not get any bet.");
                return null;
            }
        }

        // GET: api/Bets/5
        [HttpGet("{id}")]
        public async Task<BetDto> GetAsync(int id)
        {
            try
            {
                return await _betService.GetByIdAsync(id);
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not get bet with id '{ id }'.");
                return null;
            }
        }
        
        // POST: api/Bets
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]BetDto request)
        {
            try
            {
                await _betService.CreateAsync(request);
                return Ok();
            }
            catch (InvalidDataException ex)
            {
                _logger.LogInformation("Could not create new bet.");
                return Json(new { message = ex.Message });
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not create new bet.");
                return BadRequest();
            }
        }
        
        // PUT: api/Bets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody]BetUpdateDto request)
        {
            try
            {
                request.Id = id;

                await _betService.UpdateAsync(request);
                return Ok();
            }
            catch (InvalidDataException ex)
            {
                _logger.LogInformation($"Could not update bet with id '{ id }'.");
                return Json(new { message = ex.Message });
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not update bet with id '{ id }'.");
                return BadRequest();
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _betService.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidDataException ex)
            {
                _logger.LogInformation($"Could not delete bet with id '{ id }'.");
                return Json(new { message = ex.Message });
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not delete bet with id '{ id }'.");
                return BadRequest();
            }
        }
    }
}
