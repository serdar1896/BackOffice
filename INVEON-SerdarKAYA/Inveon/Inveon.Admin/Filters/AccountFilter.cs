using Inveon.Core.Interfaces.Services;
using Inveon.Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Inveon.Admin.Filters
{
    public class AccountFilter: IAsyncActionFilter 
    {
        private readonly IAccountService _accountService;
        public AccountFilter(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var model = context.ActionArguments.Values.FirstOrDefault() ?? null;
            var type= model.GetType().Name;
            if (type == typeof(RegisterDto).Name)
            {
                var user = await _accountService.GetByParamAsync(x => x.Email == ((RegisterDto)model).Email);
                if (user.Count()>0)
                {
                    ValidationMessage(context, "validationmessage", "warning", "Email Using!");
                    context.Result = new RedirectResult("/Account/Register");
                }
                else await next();
            }
            else if (type == typeof(UserDto).Name)
            {
                var user = await _accountService.GetByParamAsync(x => x.Email == ((UserDto)model).Email);
                if (user.Count() > 0)
                {
                    ValidationMessage(context, "validationmessage", "warning", "Email Using!");
                    context.Result = new RedirectResult("/Account/Create");
                }
                else await next();
            }
            else if (type == typeof(LoginDto).Name)
            { 
                var loginDto = (LoginDto)model;
                var user = _accountService.GetByParamAsync(x => x.Email == loginDto.Email && x.Password == loginDto.Password).Result.FirstOrDefault();
                if (user == null)
                {
                    ValidationMessage(context, "validationmessage", "danger", "Password Or Email Wrong!");
                    context.Result = new RedirectResult("/Account/Login");
                }
                else await next();
            }
            else await next();
        }
        public void ValidationMessage(ActionExecutingContext context, string key, string alert, string value)
                {
                    try
                    {
                        if (context.Controller is Controller controller)
                        {
                            controller.TempData.Remove(key);
                            controller.TempData.Add(key, value);
                            controller.TempData.Add("alertType", alert);
                        }
                    }
                    catch
                    {
                        Debug.WriteLine("TempDataMessage Error");
                    }

                }
           
    }
}
