using Bookmaker.Core.Utils;
using Bookmaker.Infrastructure.DTO;
using Bookmaker.Infrastructure.ServicesInterfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmaker.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]    
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private ILogger _logger;
        private IMailService _mailService;

        public UsersController(IUserService userService, ILogger<UsersController> logger, IMailService mailService)
        {
            _userService = userService;
            _logger = logger;
            _mailService = mailService;
        }

        // GET: api/Users/test@email.com
        [HttpGet("{email}")]
        public async Task<UserDto> GetAsync(string email)
        {
            try
            {
                return await _userService.GetAsync(email);                
            }
            catch (Exception)
            {                
                return null;
            }
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            try
            {
                return await _userService.GetAllAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GET: api/Users/test@email.com/bets
        [HttpGet("{email}/bets")]
        public async Task<IEnumerable<BetDto>> GetBetsAsync(string email)
        {
            try
            {
                return await _userService.GetBetsAsync(email);
            }
            catch (Exception)
            {
                _logger.LogInformation($"Could not get any bet for user with email '{ email }'.");
                return null;
            }
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody]UserCreateDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { message = "Invalid email addresss." });
                }

                await _userService.RegisterAsync(request);
            }
            catch (InvalidDataException ex)
            {
                // todel: #ask2
                return Json(new { message = ex.Message });
            }

            return Json(new { email = request.Email, username = request.Username, password = request.Password });            
        }

        // PUT: api/Users
        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateUserAsync(string email, [FromBody]UserUpdateDto request)
        {
            try
            {
                request.Email = email;
                await _userService.UpdateUserAsync(request);
            }
            catch (InvalidDataException ex)
            {
                return Json(new { message = ex.Message });
            }

            return Ok();
        }

        // DELETE: api/Users/test@email.com
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUserAsync(string email)
        {
            var userToDelete = await _userService.GetAsync(email);
            if (userToDelete == null)
            {
                _logger.LogInformation($"Cannot remove user with email address '{email}' because such a user does not exist.");
                return NotFound();
            }

            try
            {
                await _userService.RemoveAsync(email);
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception)
            {
                return BadRequest();
            }

            var mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];
            var mailTo = Startup.Configuration["mailSettings:mailToAddress"];
            await _mailService.Send(mailFrom, mailTo, "User deleted.", $"User with mail {email} has been deleted.");

            return NoContent();
        }
    }
}
