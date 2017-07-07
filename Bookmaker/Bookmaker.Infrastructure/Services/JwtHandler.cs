using Bookmaker.Infrastructure.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Bookmaker.Infrastructure.DTO;
using Bookmaker.Infrastructure.Settings;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Bookmaker.Infrastructure.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Bookmaker.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _settings;

        public JwtHandler(JwtSettings settings)
        {
            _settings = settings;
        }

        public JwtDto CreateToken(string email, string role)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimeStamp().ToString(), ClaimValueTypes.Integer64)
            };

            var expires = now.AddMinutes(_settings.ExpiryMinutes);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key)), SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _settings.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                Token = token,
                Expires = expires.ToTimeStamp(),
                Email = email
            };
        }
    }
}
