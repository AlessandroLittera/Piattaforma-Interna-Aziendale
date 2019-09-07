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
            ViewBag.lista = list;
            return View(list);

        }
        public async Task<IActionResult> ListRichAccount()
        {
            string ids = TempData["Id"] as string;
            TempData["Id"] = ids;
            ICollection<RequestAssignement> list = await accountHelper.RequestAssignementsByAccountIdAsync(ids);
            ViewBag.lista = list;
            return View(list);

        }


        public IActionResult NewRequest()
        {
            string ids = TempData["Id"] as string;
            TempData["Id"] = ids;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewRequestView(Richieste richiesta)
        {
            string ids = TempData["Id"] as string;
            TempData["Id"] = ids;
            richiesta.AccountId = ids;
            RequestAssignement request = Mapper.Map<RequestAssignement>(richiesta);
            
            if (ModelState.IsValid)
            {
                var all = await requestHelper.SaveRequestAssignement(request);
                return RedirectToAction("NewRequest");
            }
            return View();
        }
    }
}