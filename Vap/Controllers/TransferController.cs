using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Interfaces.Helpers;
using Vap.Models;

namespace Vap.Controllers
{
    public class TransferController : Controller
    {
        readonly IRequesttHelper requesttHelper;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RequestTrasf()
        {
            return View();
        }
        public async Task<IActionResult> ListRequest()
        {
            //ICollection<Richieste> list = await accountHelper.AccountsAsync();
            return View();
        }
    }
}