using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Bookmaker.Infrastructure.ServicesInterfaces;
using Bookmaker.Infrastructure.DTO;

namespace Bookmaker.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Upload")]
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserService _userService;        

        public UploadController(IHostingEnvironment hostingEnvironment, IUserService userService)
        {
            _hostingEnvironment = hostingEnvironment;
            _userService = userService;
        }

        // POST: api/Upload/avatar
        [HttpPost]
        [Authorize]
        [Route("avatar")]
        public async Task<IActionResult> UploadAvatarAsync(IFormFile file)
        {
            var upload = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "uploads");                        

            if (file == null)
            {
                return NoContent();
            }

            if (file.Length <= 0)
            {
                return NoContent();
            }

            var newFileName = Guid.NewGuid().ToString();
            using (var fileStream = new System.IO.FileStream(System.IO.Path.Combine(upload, newFileName), System.IO.FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
                        
            var updateUserData = new UserUpdateDto
            {
                Email = this.GetAuthEmail(),                
                AvatarFileName = newFileName
            };

            await _userService.UpdateUserAsync(updateUserData);

            return Json(new { fileName = newFileName });
        }

        [HttpGet]
        [Route("file/{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            return PhysicalFile(System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "uploads", fileName), "image/jpeg", fileDownloadName: null);        
        }
    }
}
