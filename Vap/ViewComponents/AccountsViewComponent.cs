using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models.Interfaces.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vap.ViewComponents
{
    public class AccountsViewComponent : ViewComponent
    {
        private readonly IAreaHelper areaHelper;
        private readonly IMapper mapper;
        public AccountsViewComponent(IAreaHelper areaHelper, IMapper mapper)
        {
            this.areaHelper = areaHelper;
            this.mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await areaHelper.ListDomainAccountsAsync());
        }
    }
}
