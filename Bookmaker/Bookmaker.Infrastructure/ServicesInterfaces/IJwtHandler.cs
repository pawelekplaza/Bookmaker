using Bookmaker.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.ServicesInterfaces
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(string email, string role);
    }
}
