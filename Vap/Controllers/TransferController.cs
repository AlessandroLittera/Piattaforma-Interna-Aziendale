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
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> RequestTrasf()
        {
            string id = TempData["Id"] as string;
            // RichiestaTrasf rich = new RichiestaTrasf();
            // ICollection<Request> list = await requestHelper.RequestsAsync();
            //ViewBag.lista = list;
            List<Richieste> ric = new List<Richieste>();
            Richieste richiestaTrasf = new Richieste();
            richiestaTrasf.Name = "Ferie";
            richiestaTrasf.From = DateTime.UtcNow;
            richiestaTrasf.To = DateTime.UtcNow;
            richiestaTrasf.AccountId = id;
            ric.Add(richiestaTrasf);
            return View(ric);
        }
       
        
        public async Task<IActionResult> ListRequest()
        {
            string id = TempData["Id"] as string;
            
            ICollection<RequestAssignement> list = await accountHelper.RequestAssignementsByAccountIdAsync(id);
            ViewBag.lista = list;
            return View(list);

        }
        public async Task<IActionResult> ListRichAccount()
        {
            string id = TempData["Id"] as string;
            ICollection<RequestAssignement> list = await accountHelper.RequestAssignementsByAccountIdAsync(id);
            ViewBag.lista = list;
            return View(list);

        }


        public IActionResult NewRequest()
        {
            string id = TempData["Id"] as string;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewRequestView(Richieste richiesta)
        {
            string id = TempData["Id"] as string;
            richiesta.AccountId = id;
            Request request = Mapper.Map<Request>(richiesta);
            
            if (ModelState.IsValid)
            {
                var all = await requestHelper.CreateRequestAsync(request);
                return RedirectToAction("NewRequest");
            }
            return View();
        }
    }
}