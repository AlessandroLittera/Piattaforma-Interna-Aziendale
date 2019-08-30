using Models;
using Models.AccountTypes;
using Models.Interfaces.Visistor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.AccountTypes
{
    public class StakeHolder : Account
    {
        public override AccountantTypes AccountType => AccountantTypes.Stakeholder;

        public override Task<L> VisitAsync<L, T>(IAccountVisitor<L, T> visitor, T item)
        {
            return visitor.DoStuffAsync(this, item);
        }
    }
}
