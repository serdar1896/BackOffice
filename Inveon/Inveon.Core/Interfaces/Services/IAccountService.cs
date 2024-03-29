﻿using Inveon.Core.Models.DTOs;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Inveon.Core.Models.Entities;

namespace Inveon.Core.Interfaces.Services
{
    public interface IAccountService:IBaseService<User>
    {
        Task<bool> Login(IHttpContextAccessor context,LoginDto loginModel, string returnUrl );
        Task<bool> Logout(IHttpContextAccessor context);
    }
}
