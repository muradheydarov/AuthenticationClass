using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationClass.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationClass.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        //public IActionResult Login(string returnUrl = null)
        //{            
        //    return View();
        //}

        //[HttpPost]

        //public async Task<IActionResult> Login(LoginViewModel loginViewModel, string ReturnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, 
        //            loginViewModel.RememberMe, false);

        //        if (signInResult.Succeeded)
        //        {
        //            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
        //            {
        //                return Redirect(ReturnUrl);
        //            }
        //            return RedirectToAction("Index", "Home");
        //        }                
        //    }

        //    return View(loginViewModel);
        //}


        //public IActionResult Register()
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser()
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Username,
                };

                var result = await userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(registerViewModel);
        }

        //public async Task<IActionResult> LogOut()
        //{
        //    await signInManager.SignOutAsync();

        //    return RedirectToAction("Login", "Account");
        //}
    }
}