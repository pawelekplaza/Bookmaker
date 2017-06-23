using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bookmaker.Infrastructure.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Bookmaker.Infrastructure.DTO;

namespace Bookmaker.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly IJwtHandler _jwtHandler;
        private readonly IEncrypter _encrypter;
        private readonly IUserService _userService;

        public AccountController(IJwtHandler jwtHandler, IEncrypter encrypter, IUserService userService)
        {
            _jwtHandler = jwtHandler;
            _userService = userService;
            _encrypter = encrypter;
        }

        // GET: api/Account
        [HttpGet]
        [Route("token")]
        public IActionResult Get()
        {
            var token = _jwtHandler.CreateToken("user1@email.com", "user");

            return Json(token);
        }

        [HttpGet]
        [Authorize]
        [Route("auth")]
        public IActionResult GetAuth()
        {
            return Content("Auth");
        }

        // GET: api/Account/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Account
        [HttpPost]        
        [Route("authenticate")]
        public async Task<IActionResult> Post([FromBody]AccountLoginDto login)
        {
            try
            {
                var user = await _userService.GetAsync(login.Email);
                var role = user.Role;

                var hash = _encrypter.GetHash(login.Password, user.Salt);
                if (hash != user.Hash)
                {
                    return Unauthorized();
                }

                var token = _jwtHandler.CreateToken(login.Email, role);
                return Json(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.Write(ex.StackTrace);
                return NoContent();
            }
        }        
        
        // PUT: api/Account/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
