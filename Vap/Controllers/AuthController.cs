using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;
using Models;
using Models.Interfaces.Helpers;
using System.Threading;
using Microsoft.AspNetCore.Identity;

namespace Vap.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserHelper userHelper;
        public AuthController(IUserHelper user)
        {
            this.userHelper = user;
        }
       
        public IActionResult Login(string returnUrl)
        {
            var callbackUrl = Url.Action("MyCallback");

            var props = new AuthenticationProperties
            {
                RedirectUri = callbackUrl,
                Items =
                {
                    { "returnUrl", returnUrl }
                }
            };

            return Challenge(props, OpenIdConnectDefaults.AuthenticationScheme);
        }
        public IActionResult LoggedOut(string returnUrl)
        {

            return RedirectToAction("Login");
        }



        public async Task<IActionResult> MyCallback(string returnUrl)
        {
            await Task.Delay(0);
            /// prendi lo user dal provider
            /// Id utente
            /// mail
            /// nome e cognome
            /// id account corrente
            /// lista account
            /// ruoli dell'account corrente
            /// 

            /*string email = User.FindFirstValue(ClaimTypes.Upn);

            var aUser = await userHelper.GetByEmailAsync(email);

            var principal = (this.User as ClaimsPrincipal);
            var identity = principal.Identity as ClaimsIdentity;
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, aUser.Id));
            //identity.AddClaim(new Claim("AccountId", aUser.Accounts.FirstOrDefault(x => x.Email == email).Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, aUser.Name + " " + aUser.Surname));

            

            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));*/
               
            
                
            //email logged
            //
            string email = User.FindFirstValue(ClaimTypes.Upn); //email logged
            ViewData["user"] = User.FindFirstValue(ClaimTypes.Upn);

            

            return RedirectToAction("All", "User");
           
        }


        public IActionResult Logged(string returnUrl)
        {
            return RedirectToAction(returnUrl);
        }


        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            }
            return View("LoggedOut");
        }

    }
}