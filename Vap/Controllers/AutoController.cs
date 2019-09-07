using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models;
using Vap.Models;

namespace Vap.Controllers
{
    public class AutoController : Controller
    {

        public IActionResult RequestAuto()
        {
            return View();
        }
        public IActionResult NewAuto()
        {
            string id = TempData["Id"] as string;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewAutoView(CreateAuto auto)
        {
            string id = TempData["Id"] as string;
            auto.AccountId = id;
            Veicle request = Mapper.Map<Veicle>(auto);

            if (ModelState.IsValid)
            {

                //var all = await requestHelper.CreateRequestAsync(request);
                return RedirectToAction("NewRequest");

            }
            return View();
        }
        public async Task<IActionResult> ListAuto()
        {
            string id = TempData["Id"] as string;

            //ICollection<VeicleAssignement> list = await 
            //ViewBag.lista = list;
            return View();
        }
    }
}