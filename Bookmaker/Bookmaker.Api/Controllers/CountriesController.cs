using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bookmaker.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using System.Collections;
using Bookmaker.Infrastructure.DTO;
using Bookmaker.Core.Utils;

namespace Bookmaker.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Countries")]
    public class CountriesController : Controller
    {
        private readonly ICountryService _countryService;
        private ILogger _logger;

        public CountriesController(ICountryService countryService, ILogger<CountriesController> logger)
        {
            _countryService = countryService;
            _logger = logger;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<IEnumerable<CountryDto>> GetAllAsync()
        {
            try
            {
                return await _countryService.GetAllAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GET: api/Countries/5
        [HttpGet("{name}")]
        public async Task<CountryDto> GetAsync(string name)
        {
            try
            {
                return await _countryService.GetAsync(name);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        // POST: api/Countries
        [HttpPost]
        public async Task<IActionResult> PostAsyc([FromBody]CountryCreateDto request)
        {
            try
            {
                await _countryService.CreateAsync(request);
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
        
        // PUT: api/Countries/5
        [HttpPut("{name}")]
        public async Task<IActionResult> PutAsync(string name, [FromBody]CountryUpdateDto request)
        {
            try
            {
                var countryToUpdate = await _countryService.GetAsync(name);
                
                if (countryToUpdate == null)
                {
                    return NotFound();
                }

                request.Id = countryToUpdate.Id;
                
                await _countryService.UpdateAsync(request);
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
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            try
            {
                var countryToDelete = await _countryService.GetAsync(name);

                if (countryToDelete == null)
                {
                    return NotFound();
                }

                await _countryService.DeleteAsync(countryToDelete.Id);
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
