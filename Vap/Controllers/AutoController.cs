using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Interfaces.Helpers;
using Vap.Models;

namespace Vap.Controllers
{
    public class AutoController : Controller
    {
        readonly IVeicleHelper veicleHelper;
        private IAccountHelper accountHelper;
        private string userId = AccountController.userId;
        private string acccountId = AccountController.accountId;
        public AutoController(IVeicleHelper veicleHelper, IAccountHelper accountHelper)
        {
            this.accountHelper = accountHelper;
            this.veicleHelper = veicleHelper;
        }
        public async Task<IActionResult> RequestAutoAsync()
        {
            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            //string ids = TempData["Id"] as string;
            //TempData["Id"] = ids;
            return View();
        }
        public async Task<IActionResult> NewAutoAsync()
        {
            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            //string ids = TempData["Id"] as string;
            //TempData["Id"] = ids;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewAutoView(CreateAuto auto)
        {
            //string ids = TempData["Id"] as string;
            //TempData["Id"] = ids;
            //auto.AccountId = ids;
            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType;
            ViewBag.accountId = acccountId;
            VeicleAssignement request = new VeicleAssignement();
            request.Account = await accountHelper.GetById(AccountController.accountId);
            request.Veicle = await veicleHelper.RetrieveByType(auto.AutoType.ToString());
            request.Note = auto.Note;
            request.From = auto.From;
            request.To = auto.To;
            
            

            if (ModelState.IsValid)
            {

                var all = await veicleHelper.SaveVeicleAssignement(request);
                return RedirectToAction("ListAuto");

            }
            return View();
        }
        public async Task<IActionResult> ListAuto()
        {
            string ids = TempData["Id"] as string;
            TempData["Id"] = ids;

            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            ICollection<VeicleAssignement> list = await veicleHelper.AllValidVeicleAssignement();

            return View(list);
        }
        public async Task<IActionResult> All()
        {
            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            return View();
        }
        public async Task<IActionResult> ListAll()
        {
            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            ICollection<VeicleAssignement> list = await veicleHelper.VeicleAssignementsAsync();
            return View(list);
        }
    }
}