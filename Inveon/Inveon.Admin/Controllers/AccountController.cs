﻿using AutoMapper;
using Inveon.Admin.Filters;
using Inveon.Core.Interfaces.Services;
using Inveon.Core.Models;
using Inveon.Core.Models.DTOs;
using Inveon.Core.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inveon.Core.Models.Entities;

namespace Inveon.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor context;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper, IHttpContextAccessor context)
        {
            _accountService = accountService;
            this.context = context;
            _mapper = mapper;
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [ServiceFilter(typeof(AccountFilter))]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginModel, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            returnUrl ??= "/Home/Index";
            await _accountService.Login(context, loginModel, returnUrl);

            return Redirect(returnUrl);
        }

        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            await _accountService.Logout(context);
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [ServiceFilter(typeof(AccountFilter))]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterDto registerModel)
        {
            var user = _mapper.Map<User>(registerModel);
            await _accountService.AddAsync(user);

            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Index()
        {
            var userList = await _accountService.GetAllAsync();
            var userDtoList = _mapper.Map<IEnumerable<UserDto>>(userList);

            return View(userDtoList.ToList());
        }
       
        public ActionResult Create()
        {
            SetRoleSelectList(EnumRole.User.Id);          
            return View();
        }

        [HttpPost]
        [Obsolete]
        [ServiceFilter(typeof(AccountFilter))]
        public async Task<IActionResult> Create(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _accountService.AddAsync(user);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _accountService.GetByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user);
            SetRoleSelectList(userDto.Role);

            return View(userDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(AccountFilter))]
        public async Task<IActionResult> Edit(string id, UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _accountService.UpdateAsync(id, user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _accountService.GetByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user);

            return View(userDto);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            var user = await _accountService.GetByIdAsync(id);
            await _accountService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _accountService.GetByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user);

            return View(userDto);
        }

        [AllowAnonymous]
        public IActionResult Denied()
        {
            return View();
        }
        private void SetRoleSelectList(int selectedValue=0)
        {
            var selectList = new SelectList(EnumRole.RoleModelList, "Id", "Text", selectedValue);
            ViewBag.RoleList = selectList;
        }
    }
}