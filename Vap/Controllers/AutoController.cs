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

        public AutoController(IVeicleHelper veicleHelper, IAccountHelper accountHelper)
        {
            this.accountHelper = accountHelper;
            this.veicleHelper = veicleHelper;
        }
        public IActionResult RequestAuto()
        {
            //string ids = TempData["Id"] as string;
            //TempData["Id"] = ids;
            return View();
        }
        public IActionResult NewAuto()
        {
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
            VeicleAssignement request = new VeicleAssignement();
            request.Account = await accountHelper.GetById(AccountController.accountId);
            request.Veicle = await veicleHelper.RetrieveByType(auto.AutoType.ToString());
            request.Note = auto.Note;
            request.From = auto.From;
            request.To = auto.To;
            
            

            if (ModelState.IsValid)
            {

                var all = await veicleHelper.SaveVeicleAssignement(request);
                return RedirectToAction("NewRequest");

            }
            return View();
        }
        public async Task<IActionResult> ListAuto()
        {
            string ids = TempData["Id"] as string;
            TempData["Id"] = ids;

            
            ICollection<VeicleAssignement> list = await veicleHelper.AllValidVeicleAssignement();

            return View(list);
        }
    }
}