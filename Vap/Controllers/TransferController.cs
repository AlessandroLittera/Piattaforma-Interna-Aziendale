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
        private string userid = AccountController.userId;
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
            string ids = TempData["Id"] as string;
            TempData["Id"] = ids;
            // RichiestaTrasf rich = new RichiestaTrasf();
            // ICollection<Request> list = await requestHelper.RequestsAsync();
            //ViewBag.lista = list;
            List<Richieste> ric = new List<Richieste>();
            Richieste richiestaTrasf = new Richieste();
            richiestaTrasf.Name = "Ferie";
            richiestaTrasf.From = DateTime.UtcNow;
            richiestaTrasf.To = DateTime.UtcNow;
            richiestaTrasf.AccountId = ids;
            ric.Add(richiestaTrasf);
            return View(ric);
        }
       
        
        public async Task<IActionResult> ListRequest()
        {
          

            ICollection<RequestAssignement> list = await accountHelper.RequestAssignementsByAccountIdAsync(this.userid);
            return View(list);

        }
        public async Task<IActionResult> ListRichAccount()
        {

            ICollection<RequestAssignement> list = await accountHelper.RequestAssignementsByAccountIdAsync(this.userid);
            return View(list);

        }


        public IActionResult NewRequest()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewRequestView(Richieste richiesta)
        {


            RequestAssignement request = new RequestAssignement();
            request.Account = await accountHelper.GetById(this.acccountId);
            request.Request = await requestHelper.RetrieveByType(richiesta.RequestType.ToString());
            request.Note = richiesta.Note;
            request.From = richiesta.From;
            request.To = request.To;
            request.IsValid = request.IsValid;
            if (ModelState.IsValid)
            {
                var all = await requestHelper.SaveRequestAssignement(request);
                return RedirectToAction("NewRequest");
            }
            return View();
        }
    }
}