using Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Vap.Models;
using static Vap.Models.CreateAuto;

namespace Vap.Extensions
{
    public static class AutoExtensions
    {
        //public static ICollection<SelectListItem> Selectable(CreateAuto auto)
        //{
        //    var aList = new List<SelectListItem>
        //    {
        //        new SelectListItem() { Text = "BMW_classeE", Value = AutoTypes.BMW_classeE.ToString() , Selected = AutoTypes.BMW_classeE == auto.AutoType },
        //        new SelectListItem() { Text = "FIAT_500L", Value = AutoTypes.FIAT_500L.ToString() , Selected = AutoTypes.FIAT_500L == auto.AutoType },
        //        new SelectListItem() { Text = "POLO", Value = AutoTypes.POLO.ToString() , Selected = AutoTypes.POLO == auto.AutoType },
        //        new SelectListItem() { Text = "TESLA", Value = AutoTypes.TESLA.ToString() , Selected = AutoTypes.TESLA == auto.AutoType },
        //    };
            
        //    return aList;
        //}
        //public static AutoTypes Type(string type)
        //{
        //    switch (type)
        //    {
        //        case "0": { return AutoTypes.BMW_classeE; }
        //        case "1": { return AutoTypes.FIAT_500L; }
        //        case "2": { return AutoTypes.POLO; }
        //        case "3": { return AutoTypes.TESLA; }
        //        default: { return AutoTypes.POLO; }
        //    }
        //}
    }
}
