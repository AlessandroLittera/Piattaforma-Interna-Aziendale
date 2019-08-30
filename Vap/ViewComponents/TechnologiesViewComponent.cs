using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models.Interfaces.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vap.ViewComponents
{
    public class TechnologiesViewComponent: ViewComponent
    {
        private readonly ITechnologyHelper technologyHelper;
        private readonly IMapper mapper;
        public TechnologiesViewComponent(ITechnologyHelper technologyHelper, IMapper mapper)
        {
            this.technologyHelper = technologyHelper;
            this.mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View(await technologyHelper.ListDomainAccountsAsync());
        }
    }
}
