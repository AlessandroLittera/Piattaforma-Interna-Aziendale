using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Helper.Web.HatHelper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Interfaces.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting.Server;
using System.Net.Http.Headers;
using AutoMapper;
using Vap.Models;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;
using System;
using Microsoft.Extensions.Configuration;

namespace Vap.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IResolutorFacade resolutorFacade;
        private readonly IMapper mapper;
        private readonly IFileService fileService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UserController(IUserHelper userHelper, IResolutorFacade resolutorFacade, IFileService fileService, IHostingEnvironment environment, IMapper mapper)
        {
            this.resolutorFacade = resolutorFacade;
            this.userHelper = userHelper;
            this.mapper = mapper;
            this.fileService = fileService;
            _hostingEnvironment = environment;
        }

        public async Task<IActionResult> EditUserAccount()
        {

            await Task.Delay(0);
            return View();
        }



        public IActionResult Profile(User u)
        {
            return View(u);
        }

        public async Task<IActionResult> NewUser(AccountantTypes accountantTypes)
        {

            await Task.Delay(0);
            Assignement assignement = new Assignement
            {
                Account = Account.GetInstanceOf(AccountantTypes.Admin)
            };
            
            return View(mapper.Map<CreateUser>(assignement));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewUserView(CreateUser createUser)
        {

            var assignment = mapper.Map<Assignement>(createUser);
    
            var a = await resolutorFacade.CreateUserAndDefaultAccount(assignment);
            return RedirectToAction("All");


        }


        public async Task<IActionResult> Edit(string id)
        {
            User aUser = new User();
            //ViewBag.UserId = id;
            aUser = await userHelper.GetAsync(new User() { Id = id });
            aUser.Assignements = await userHelper.AssignementsByUserIdAsync(id);
            return View(aUser);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserView(User u)
        {

            var all = await userHelper.EditAsync(u);

            await HttpContext.SignInAsync(User);
            return RedirectToAction("All");
        }

        public async Task<IActionResult> All(string Id)
        {
            //ViewBag.Id = Id;
            ICollection<User> user = await userHelper.UsersAsync();
            //    await HttpContext.SignInAsync(User);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(User u)
        {
            await userHelper.DeleteAsync(u);
            return Content("");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAssignement(string id)
        {
            await userHelper.DeleteAssignementAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ListUserFromAccount(string email)
        {
            ICollection<User> list = await userHelper.GetByEmailAsync(email);
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAssignement(string userId, List<string> accountsId)
        {
            await userHelper.SetAssignementAsync(userId, accountsId);
            await HttpContext.SignInAsync(User);
            return RedirectToAction("Edit/" + userId);
        }

        public async Task<IActionResult> ListAccounts(string id)
        {
            //ViewBag.UserId = id;

            ListAccountsForUser list = new ListAccountsForUser();
            list.Accounts= await userHelper.AccountsNotPresentAsync(id);
            list.user = await userHelper.GetById(id);
            return PartialView("_AccountsForUser", list);
        }

        [HttpPost]
        public async Task<IActionResult> LogoUpload(IFormFile file)
        {
            var url = await fileService.SaveAsync("uploads", file.FileName, file.OpenReadStream());
            return Ok(url);
        }
    }
}
