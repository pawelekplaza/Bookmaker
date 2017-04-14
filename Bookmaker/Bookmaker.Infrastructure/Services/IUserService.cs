﻿using Bookmaker.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.Services
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(string email);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task RemoveAsync(string email);
        Task RegisterAsync(UserCreateDto user);
        Task UpdateUserAsync(UserUpdateDto newUserData);
    }
}
