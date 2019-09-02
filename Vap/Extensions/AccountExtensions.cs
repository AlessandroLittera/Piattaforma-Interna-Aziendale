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
                new SelectListItem() { Text = Resource.DPO, Value = AccountantTypes.Admin.ToString(), Selected = AccountantTypes.Admin == account.AccountType },
                new SelectListItem() { Text = Resource.StakeHolder, Value = AccountantTypes.Standard.ToString(), Selected = AccountantTypes.Standard == account.AccountType },
            };

            return aList;
        }
       public static AccountantTypes Type (string type)
        {
            switch (type)
            {
                case "0": { return AccountantTypes.Admin; }
                case "1": { return AccountantTypes.Standard; }
                default: { return AccountantTypes.Standard; }
            }
        }
    }
}
