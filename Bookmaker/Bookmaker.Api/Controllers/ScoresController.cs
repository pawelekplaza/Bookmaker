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
    [Route("api/Scores")]
    public class ScoresController : Controller
    {
        private readonly IScoreService _scoreService;
        private readonly ILogger _logger;

        public ScoresController(IScoreService scoreService, ILogger<ScoresController> logger)
        {
            _scoreService = scoreService;
            _logger = logger;
        }

        // GET: api/Scores
        [HttpGet]
        public async Task<IEnumerable<ScoreDto>> GetAllAsync()
        {
            try
            {
                return await _scoreService.GetAllAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GET: api/Scores/5
        [HttpGet("{id}")]
        public async Task<ScoreDto> GetAsync(int id)
        {
            try
            {
                return await _scoreService.GetAsync(id);                
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GET: api/Scores/5/
        [HttpGet("{id}/bets")]
        public async Task<IEnumerable<BetDto>> GetBetsAsync(int id)
        {
            try
            {
                return await _scoreService.GetBetsAsync(id);
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not get any bet for score with id '{ id }'.");
                return null;
            }
        }
        
        // POST: api/Scores
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ScoreCreateDto request)
        {
            try
            {
                await _scoreService.CreateAsync(request);
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
        
        // PUT: api/Scores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody]ScoreUpdateDto request)
        {
            try
            {
                var scoreToUpdate = await _scoreService.GetAsync(id);

                if (scoreToUpdate == null)
                {
                    return NotFound();
                }

                request.Id = id;

                await _scoreService.UpdateAsync(request);
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
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var scoreToDelete = await _scoreService.GetAsync(id);

                if (scoreToDelete == null)
                {
                    return NotFound();
                }

                await _scoreService.DeleteAsync(id);
                return NoContent();
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
    }
}
