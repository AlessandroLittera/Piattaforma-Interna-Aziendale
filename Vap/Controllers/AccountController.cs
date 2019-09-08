using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models;
using Models.AccountTypes;
using Models.Interfaces.Helpers;
using Models.Interfaces.Providers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication;
using AutoMapper;
using Vap.Models;
using Microsoft.AspNetCore.Identity;

namespace Vap.Controllers
{


    // [Authorize(Roles = Account.God)]
    
    public class AccountController : Controller
    {
        readonly IAccountHelper accountHelper;
        readonly IUserHelper userHelper;
        private IMapper mapper;
        public static string userId;
        public static string accountId;
        User userDto = new User();
        
        public AccountController(IAccountHelper accountHelper, IUserHelper userHelper, IMapper mapper)
        {
            this.accountHelper = accountHelper;
            this.userHelper = userHelper;
            this.mapper = mapper;
        }

        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                this.ModelState.AddModelError("Password", "Campi errati");
                return View(model);
            }

            if (model.Email == null || model.Password == null)
            {
                this.ModelState.AddModelError("Password", "Campi errati");
                return View("Login", "Account");
            }

            userDto = await accountHelper.CheckUser(model.Email, model.Password);
            Account account = await accountHelper.GetByEmailAsync(model.Email);
            AccountController.accountId = account.Id;
            if (userDto == null)
            {
                ModelState.AddModelError("", "Errore");
                return View(model);
            }


            if (returnUrl != "/" && !string.IsNullOrWhiteSpace(returnUrl) && !(returnUrl.ToLower().StartsWith("http://") || returnUrl.ToLower().StartsWith("https://")))
            {
                return Redirect(returnUrl);
            }
            else
            {
                TempData["Id"] = userDto.Id;
                AccountController.userId = userDto.Id;
                return RedirectToAction("All", "User");
                
            }
        }
        public async Task<IActionResult> Edit(string id)
        {
            string ids = userId;
            TempData["Id"] = ids;
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem("DPO", "DPO") { Selected = true },
                new SelectListItem("StakeHolder", "StakeHolder"),
                new SelectListItem("RSGSI", "RSGSI"),
                new SelectListItem("Standard", "Standard")
            };
            

            Account account = await accountHelper.GetById(id);

            SelectList li = new SelectList(items);
            ViewBag.AccountType = items;
            account.Assignements = await accountHelper.AssignementsbyAccountIdAsync(id);
            //ViewBag.AccountId = id;
            await Task.Delay(0);
            return View(account);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccountView(Account account)
        {
            Account accountt = await accountHelper.GetById(accountId);
            ViewBag.accountType = accountt.AccountType.ToString();
            string ids = userId;
            TempData["Id"] = ids;
            var edit = await accountHelper.EditAsync(account);
            await HttpContext.SignInAsync(User);
            return RedirectToAction("ListAccounts");
        }

        public async Task<IActionResult> DeleteAccount(string id)
        {
            string ids = userId;
            TempData["Id"] = ids;
            Account account = await accountHelper.GetById(accountId);
            ViewBag.accountType = account.AccountType.ToString();
            var edit = await accountHelper.DeleteAsync(id);
            return RedirectToAction("ListAccounts");
        }
        public async Task<IActionResult> DeleteAssignement(string id)
        {
            string ids = userId;
            TempData["Id"] = ids;
            var edit = await accountHelper.DeleteAssignement(id);
            return RedirectToAction("ListAccounts");
        }

        public async Task<IActionResult> ListUsers(string id)
        {
            string ids = userId;
            TempData["Id"] = ids;
            Account account = await accountHelper.GetById(accountId);
            ViewBag.accountType = account.AccountType.ToString();
            ListUsersForAccount list = new ListUsersForAccount();
            list.Users = await accountHelper.UsersNotPresentAsync(id);
            list.account = await accountHelper.GetById(id);
            return PartialView("_UsersForAccount", list);
        }



        public async Task<IActionResult> AccountAssignement(string accountsId, List<string> usersId)
        {
            string ids = userId;
            TempData["Id"] = ids;
            Account account = await accountHelper.GetById(accountId);
            ViewBag.accountType = account.AccountType.ToString();
            var edit = await accountHelper.SetAssignementAsync(accountsId, usersId);
            await HttpContext.SignInAsync(User);
            return RedirectToAction("ListAccounts");
        }

        public async Task<IActionResult> ListAccounts()
        {
            string ids = userId;
            TempData["Id"] = ids;
            Account account = await accountHelper.GetById(accountId);
            ViewBag.accountType = account.AccountType.ToString();
            ICollection<Account> list = await accountHelper.AccountsAsync();
            return View(list);
        }


        [HttpPost]
        public async Task<IActionResult> ListAccountFromUser(User user)
        {
            string ids = userId;
            TempData["Id"] = ids;
            ICollection<Account> list = await accountHelper.GetByUserAsync(user);
            return View(list);
        }

        public async Task<IActionResult> NewAccount(AccountantTypes accountant)
        {
            string ids = userId;
            TempData["Id"] = ids;
            Account account = await accountHelper.GetById(accountId);
            ViewBag.accountType = account.AccountType.ToString();
            await Task.Delay(0);
            // da vedere bene
            // non  bello il modo in cui setto i valori e poi il passaggio con user
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem("ADMIN", AccountantTypes.Admin.ToString()),
                new SelectListItem("Standard", AccountantTypes.Standard.ToString())
            };
            SelectList li = new SelectList(items);
            ViewBag.AccountType = items;
            Account a = Account.GetInstanceOf(AccountantTypes.Standard);

            return View(mapper.Map<CreateAccount>(a));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewAccountView(CreateAccount createAccount)
        {
            string ids = userId;
            TempData["Id"] = ids;
            Account accounts = await accountHelper.GetById(accountId);
            ViewBag.accountType = accounts.AccountType.ToString();
            Account account = mapper.Map<Account>(createAccount);
            if (ModelState.IsValid)
            {

                var all = await accountHelper.CreateAccountAsync(account);
                return RedirectToAction("ListAccounts");

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveAssignement(string accountId, List<string> usersId)
        {
            string ids = userId;
            TempData["Id"] = ids;
            await accountHelper.SetAssignementAsync(accountId, usersId);
            await HttpContext.SignInAsync(User); //per la lista degli account by user
            return RedirectToAction("Edit/" + accountId);
        }
       


    }
}