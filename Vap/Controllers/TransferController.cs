using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Interfaces.Helpers;
using Vap.Models;

namespace Vap.Controllers
{
    public class TransferController : Controller
    {
        readonly IRequestHelper requestHelper;
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> RequestTrasf()
        {
            string id = TempData["Id"] as string;
            // ICollection<Request> list = await requestHelper.RequestsAsync();
            //ViewBag.lista = list;
            return View();
        }
        public async Task<IActionResult> ListRequest()
        {
            string id = TempData["Id"] as string;
            ICollection<Request> list = await requestHelper.RequestsAsync();
            ViewBag.lista = list;
            return View(list);
        }
    }
}