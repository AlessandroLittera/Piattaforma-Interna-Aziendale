using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vap.Models
{
    public class ReportViewModelController : Controller
    {
        public string Name { set; get; }

        [HttpPost]
        public ActionResult Report(string reportName)
            {
                return View(new ReportViewModelController());
            }
}
}