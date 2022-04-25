﻿using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Mvc.Admin.ViewModels.FrameworkUserVms;
using System.Collections.Generic;
using LQClass.Admin.ViewModel.HomeVMs;

namespace LQClass.Admin.Controllers
{
    [AllRights]
    public class LoginController : BaseController
    {

        [Public]
        [ActionDescription("Login")]
        public IActionResult Login()
        {
            LoginVM vm = Wtm.CreateVM<LoginVM>();
            vm.Redirect = HttpContext.Request.Query["ReturnUrl"];
            if (Wtm.ConfigInfo.IsQuickDebug == true)
            {
                vm.ITCode = "admin";
                vm.Password = "000000";
            }
            return View(vm);
        }

        [Public]
        [HttpPost]
        public async Task<ActionResult> Login(LoginVM vm)
        {
            if (Wtm.ConfigInfo.IsQuickDebug == false)
            {
                var verifyCode = HttpContext.Session.Get<string>("verify_code");
                if (string.IsNullOrEmpty(verifyCode) || verifyCode.ToLower() != vm.VerifyCode.ToLower())
                {
                    vm.MSD.AddModelError("", Localizer["Login.ValidationFail"]);
                    return View(vm);
                }
            }
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("account", vm.ITCode);
            data.Add("password", vm.Password);
            data.Add("withmenu", "false");
            var user = await Wtm.CallAPI<LoginUserInfo>("", Request.Scheme+"://"+ Request.Host.ToString()+"/api/_account/login", HttpMethodEnum.POST, data);
            if (user?.Data == null)
            {
                return View(vm);
            }
            else
            {
                Wtm.LoginUserInfo = user.Data;
                string url = string.Empty;
                if (!string.IsNullOrEmpty(vm.Redirect))
                {
                    url = vm.Redirect;
                }
                else
                {
                    url = "/";
                }

                AuthenticationProperties properties = null;
                if (vm.RememberLogin)
                {
                    properties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30))
                    };
                }

                var principal = user.Data.CreatePrincipal();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
                return Redirect(HttpUtility.UrlDecode(url));
            }
        }


        [Public]
        public IActionResult Reg()
        {
            var vm = Wtm.CreateVM<RegVM>();
            return PartialView(vm);
        }

        [Public]
        [HttpPost]
        public IActionResult Reg(RegVM vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                var rv = vm.DoReg();
                if (rv == true)
                {
                    return FFResult().CloseDialog().Message(Localizer["Reg.Success"]);
                }
                else
                {
                    return PartialView(vm);
                }
            }
        }

        [AllRights]
        [ActionDescription("Logout")]
        public async Task Logout()
        {
            await Wtm.RemoveUserCache(Wtm.LoginUserInfo.ITCode);
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Redirect("/");
        }

        [AllRights]
        [ActionDescription("ChangePassword")]
        public ActionResult ChangePassword()
        {
            var vm = Wtm.CreateVM<ChangePasswordVM>();
            vm.ITCode = Wtm.LoginUserInfo.ITCode;
            return PartialView(vm);
        }

        [AllRights]
        [HttpPost]
        [ActionDescription("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordVM vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                vm.DoChange();
                return FFResult().CloseDialog().Alert(Localizer["Login.ChangePasswordSuccess"]);
            }
        }

    }
}
