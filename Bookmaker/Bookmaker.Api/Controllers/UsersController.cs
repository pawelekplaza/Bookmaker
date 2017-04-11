using Bookmaker.Infrastructure.DTO;
using Bookmaker.Infrastructure.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmaker.Api.Controllers
{
    [Route("[controller]")]    
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

        [HttpGet("get/all")]
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

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody]UserCreateDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _userService.RegisterAsync(request.Email, request.Username, request.Password);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }

            return Json(new { email = request.Email, username = request.Username, password = request.Password });
        }

        [HttpPatch("{email}")]
        public async Task<IActionResult> UpdateUserAsync(string email, 
            [FromBody] JsonPatchDocument<UserUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var user = await _userService.GetForUpdateAsync(email);
            if (user == null)
            {
                return BadRequest();
            }

            var userToUpdate = new UserUpdateDto
            {
                Username = user.Username,
                Password = user.Password
            };

            patchDoc.ApplyTo(userToUpdate, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _userService.UpdateUserAsync(email, userToUpdate.Username, userToUpdate.Password);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            TryValidateModel(userToUpdate);

            return NoContent();
        }

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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];
            var mailTo = Startup.Configuration["mailSettings:mailToAddress"];
            await _mailService.Send(mailFrom, mailTo, "User deleted.", $"User with mail {email} has been deleted.");

            return NoContent();
        }
    }
}
