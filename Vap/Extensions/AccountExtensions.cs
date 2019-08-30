using Models;
using Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Vap.Extensions
{
    public static class AccountExtensions
    {
        public static ICollection<SelectListItem> Selectable(this Account account)
        {
            var aList = new List<SelectListItem>
            {
                new SelectListItem() { Text = Resource.DPO, Value = AccountantTypes.DPO.ToString(), Selected = AccountantTypes.DPO == account.AccountType },
                new SelectListItem() { Text = Resource.StakeHolder, Value = AccountantTypes.Stakeholder.ToString(), Selected = AccountantTypes.Stakeholder == account.AccountType },
                new SelectListItem() { Text = Resource.RSGSI, Value = AccountantTypes.RSGSI.ToString(), Selected = AccountantTypes.RSGSI == account.AccountType },
                new SelectListItem() { Text = Resource.Standard, Value = AccountantTypes.Standard.ToString(), Selected = AccountantTypes.Standard == account.AccountType },
                new SelectListItem() { Text = Resource.God, Value = AccountantTypes.God.ToString(), Selected = AccountantTypes.God == account.AccountType }
            };

            return aList;
        }
       public static AccountantTypes Type (string type)
        {
            switch (type)
            {
                case "0": { return AccountantTypes.DPO; }
                case "1": { return AccountantTypes.Stakeholder; }
                case "3": { return AccountantTypes.RSGSI; }
                case "4": { return AccountantTypes.God;  }
                default: { return AccountantTypes.Standard; }
            }
        }
    }
}
