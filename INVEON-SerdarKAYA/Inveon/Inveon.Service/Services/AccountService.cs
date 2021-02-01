using Inveon.Core.Interfaces.Repositories;
using Inveon.Core.Interfaces.Services;
using Inveon.Core.Models;
using Inveon.Core.Models.DTOs;
using Inveon.Core.Models.Enum;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Inveon.Service.Services
{
    public class AccountService : BaseService<User>, IAccountService
    {
        public AccountService(IBaseRepository<User> repository) : base(repository)
        {
        }
        public async Task<bool> Login(IHttpContextAccessor context, LoginDto loginModel, string returnUrl)
        {
            try
            {

                var user = base.GetByParamAsync(x => 
                x.Email == loginModel.Email &&
                x.Password == loginModel.Password && 
                x.Status==true).Result.FirstOrDefault();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, EnumRole.RoleModelList.FirstOrDefault(x => x.Id == user.Role).Text)
                };
                var properties = new AuthenticationProperties
                {
                    RedirectUri = returnUrl,
                    IsPersistent = loginModel.RememberMe                   
                };
                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await AuthenticationHttpContextExtensions.SignInAsync(context.HttpContext, principal, properties);

                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }
        public async Task<bool> Logout(IHttpContextAccessor context)
        {
            try
            {
                if (context.HttpContext.Request.Cookies.Count > 0)
                {
                    var siteCookies = context.HttpContext.Request.Cookies.Where(c => c.Key.Contains(".AspNetCore.") || c.Key.Contains("Microsoft.Authentication"));
                    foreach (var cookie in siteCookies)
                    {
                        context.HttpContext.Response.Cookies.Delete(cookie.Key);
                    }
                }
                await context.HttpContext.SignOutAsync();
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);                
            }
        }



    }
}
