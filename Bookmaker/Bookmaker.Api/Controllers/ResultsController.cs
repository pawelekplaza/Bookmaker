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
    [Route("api/Results")]
    public class ResultsController : Controller
    {
        private readonly IResultService _resultService;
        private readonly ILogger _logger;

        public ResultsController(IResultService resultService, ILogger<ResultsController> logger)
        {
            _resultService = resultService;
            _logger = logger;
        }

        // GET: api/Results
        [HttpGet]
        public async Task<IEnumerable<ResultDto>> GetAllAsync()
        {
            try
            {
                return await _resultService.GetAllAsync();
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not get any result.");
                return null;
            }
        }

        // GET: api/Results/5
        [HttpGet("{id}")]
        public async Task<ResultDto> GetAsync(int id)
        {
            try
            {
                return await _resultService.GetByIdAsync(id);
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not get result with id '{ id }'.");
                return null;
            }
        }
        
        // POST: api/Results
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ResultDto request)
        {
            try
            {
                await _resultService.CreateAsync(request);
                return Ok();
            }
            catch (InvalidDataException ex)
            {
                _logger.LogInformation("Could not create new result.");
                return Json(new { message = ex.Message });
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not create new result.");
                return BadRequest();
            }
        }
        
        // PUT: api/Results/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody]ResultUpdateDto request)
        {
            try
            {
                request.Id = id;

                await _resultService.UpdateAsync(request);
                return Ok();
            }
            catch (InvalidDataException ex)
            {
                _logger.LogInformation($"Could not update result with id '{ id }'.");
                return Json(new { message = ex.Message });
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not update result with id '{ id }'.");
                return BadRequest();
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _resultService.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidDataException ex)
            {
                _logger.LogInformation($"Could not delete result with id '{ id }'.");
                return Json(new { message = ex.Message });
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not delete result with id '{ id }'.");
                return BadRequest();
            }
        }
    }
}
