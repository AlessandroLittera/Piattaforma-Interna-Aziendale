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
    public class TransferController : Controller
    {
        readonly IRequestHelper requestHelper;
        readonly IAccountHelper accountHelper;
        public string userid = AccountController.userId;
        private string acccountId = AccountController.accountId;
        public TransferController(IRequestHelper requestHelper, IAccountHelper accountHelper)
        {
            this.requestHelper = requestHelper;
            this.accountHelper = accountHelper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> RequestTrasf()
        {
            await Task.Delay(0);
            string ids = this.userid;
            TempData["Id"] = ids;
            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            // RichiestaTrasf rich = new RichiestaTrasf();
            // ICollection<Request> list = await requestHelper.RequestsAsync();
            //ViewBag.lista = list;
            List<Richieste> ric = new List<Richieste>();
            Richieste richiestaTrasf = new Richieste();
            richiestaTrasf.AccountId = ids;
            ric.Add(richiestaTrasf);
            return View(ric);
        }
       
        
        public async Task<IActionResult> ListRequest()
        {

            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            ICollection<RequestAssignement> list = await accountHelper.RequestAssignementsByAccountIdAsync(this.userid);
            return View(list);

        }
        public async Task<IActionResult> ListRichAccount()
        {
            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            ICollection<RequestAssignement> list = await accountHelper.RequestAssignementsByAccountIdAsync(this.userid);
            return View(list);

        }


        public IActionResult NewRequest()
        {
            return View();
        }
        public async Task<IActionResult> ChangeType(string id)
        {
            var a = await requestHelper.RequestAssignementsValidByRequestIdAsync(id);
            return RedirectToAction("ListRequest");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewRequestView(Richieste richiesta)
        {

            Account account = await accountHelper.GetById(acccountId);
            ViewBag.accountType = account.AccountType.ToString();
            RequestAssignement request = new RequestAssignement();
            request.Account = await accountHelper.GetById(this.acccountId);
            request.Request = await requestHelper.RetrieveByType(richiesta.RequestType.ToString());
            request.Note = richiesta.Note;
            request.From = richiesta.StartDate;
            request.To = richiesta.To;
            request.IsValid = request.IsValid;
            if (ModelState.IsValid)
            {
                var all = await requestHelper.SaveRequestAssignement(request);
                return RedirectToAction("NewRequest");
            }
            return View();
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
            ICollection<RequestAssignement> list = await requestHelper.RequestAssignementsAsync();
            return View(list);
        }
    }
}