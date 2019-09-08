﻿using System;
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
        private string userId = AccountController.userId;
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
            string ids = userId;
            TempData["Id"] = ids;
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
          

            ICollection<RequestAssignement> list = await accountHelper.RequestAssignementsByAccountIdAsync(this.userId);
            return View(list);

        }
        public async Task<IActionResult> ListRichAccount()
        {

            ICollection<RequestAssignement> list = await accountHelper.RequestAssignementsByAccountIdAsync(this.userId);
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
            request.Account = await accountHelper.GetById(this.userId);
            request.Request = await requestHelper.RetrieveByType(richiesta.RequestType.ToString());
            request.Note = richiesta.Note;
            request.From = richiesta.StartDate;
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