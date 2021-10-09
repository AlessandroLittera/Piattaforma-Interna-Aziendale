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
        private IAccountHelper accountHelper;
        private readonly IFileService fileService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private string userId = AccountController.userId;
        private string acccountId = AccountController.accountId;
        public UserController(IUserHelper userHelper, IAccountHelper accountHelper, IResolutorFacade resolutorFacade, IFileService fileService, IHostingEnvironment environment, IMapper mapper)
        {
            this.resolutorFacade = resolutorFacade;
            this.userHelper = userHelper;
            this.mapper = mapper;
            this.fileService = fileService;
            _hostingEnvironment = environment;
            this.accountHelper = accountHelper;
        }

        [HttpPost("/User")]
        public async Task<IActionResult> EditUserAccount()
        {
            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            string ids = userId;
            TempData["Id"] = ids;
            await Task.Delay(0);
            return View();
        }


        
        public IActionResult Profile(User u)
        {
            //Account account = await accountHelper.GetById(acccountId);
            //ViewBag.accountType = account.AccountType.ToString();
            string ids = userId;
            TempData["Id"] = ids;
            return View(u);
        }

        [HttpPost]
        public async Task<IActionResult> NewUser(AccountantTypes accountantTypes)
        {
            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            string ids = userId;
            TempData["Id"] = ids;
            await Task.Delay(0);
            Assignement assignement = new Assignement
            {
                Account = Account.GetInstanceOf(AccountantTypes.Admin)
            };
            
            return View(mapper.Map<CreateUser>(assignement));
        }

        [HttpPost("/View")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewUserView(CreateUser createUser)
        {
            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            string ids = userId;
            TempData["Id"] = ids;
            var assignment = mapper.Map<Assignement>(createUser);
    
            var a = await resolutorFacade.CreateUserAndDefaultAccount(assignment);
            return RedirectToAction("All");


        }

        [HttpPut]
        public async Task<IActionResult> Edit(string id)
        {
            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            string ids = userId;
            TempData["Id"] = ids;
            User aUser = new User();
            //ViewBag.UserId = id;
            aUser = await userHelper.GetAsync(new User() { Id = id });
            aUser.Assignements = await userHelper.AssignementsByUserIdAsync(id);
            return View(aUser);
        }

        [HttpPut("/EditUserView")]
        public async Task<IActionResult> EditUserView(User u)
        {
            string ids = userId; 
            TempData["Id"] = ids;

            var all = await userHelper.EditAsync(u);

            await HttpContext.SignInAsync(User);
            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> All(string Id)
        {
            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            ViewBag.accountId = acccountId;
            string ids = userId;
            TempData["Id"] = ids;
            ICollection<User> user = await userHelper.UsersAsync();
            //    await HttpContext.SignInAsync(User);
            return View(user);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConfirm(User u)
        {
            string ids = userId;
            TempData["Id"] = ids;
            await userHelper.DeleteAsync(u);
            return Content("");
        }

        [HttpDelete("/Assignement")]
        public async Task<IActionResult> DeleteAssignement(string id)
        {
            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            string ids = userId;
            TempData["Id"] = ids;
            await userHelper.DeleteAssignementAsync(ids);
            return Ok();
        }

        [HttpGet("/{email}")]
        public async Task<IActionResult> ListUserFromAccount(string email)
        {
            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            string ids = userId;
            TempData["Id"] = ids;
            ICollection<User> list = await userHelper.GetByEmailAsync(email);
            return View(list);
        }

        [HttpPost("/Assignement")]
        public async Task<IActionResult> SaveAssignement(string userId, List<string> accountsId)
        {
            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            string ids = userId;
            TempData["Id"] = ids;
            await userHelper.SetAssignementAsync(userId, accountsId);
            await HttpContext.SignInAsync(User);
            return RedirectToAction("Edit/" + userId);
        }

        [HttpGet("/Accounts")]
        public async Task<IActionResult> ListAccounts(string id)
        {
            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            string ids = userId;
            TempData["Id"] = ids;
            ListAccountsForUser list = new ListAccountsForUser();
            list.Accounts= await userHelper.AccountsNotPresentAsync(ids);
            list.user = await userHelper.GetById(ids);
            return PartialView("_AccountsForUser", list);
        }

        [HttpPost("/Logo")]
        public async Task<IActionResult> LogoUpload(IFormFile file)
        {
            string ids = userId;
            TempData["Id"] = ids;
            var url = await fileService.SaveAsync("uploads", file.FileName, file.OpenReadStream());
            return Ok(url);
        }
    }
}
