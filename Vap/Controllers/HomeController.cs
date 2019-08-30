using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Helper.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.AccountTypes;
using Models.Interfaces.Helpers;
using Vap.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Vap.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IMapper mapper;
        private readonly IAccountHelper accountHelper;
        public HomeController(IUserHelper userHelper,IMapper mapper, IAccountHelper accountHelper)
        {
            this.accountHelper = accountHelper;
            this.mapper = mapper;
            this.userHelper = userHelper;
        }

        public async Task<IActionResult> Index()
        {
            
            await HttpContext.SignInAsync( User);
            await Task.Delay(0);
            return View();
        }
        
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Login()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        public IActionResult AuthID()
        {
            return View();
        }
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
