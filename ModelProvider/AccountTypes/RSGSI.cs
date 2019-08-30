using Models.AccountTypes;
using Models.Interfaces.Visistor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.AccountTypes
{
    public class RSGSI : Account
    {
        public override AccountantTypes AccountType => AccountantTypes.RSGSI;

        public override Task<L> VisitAsync<L, T>(IAccountVisitor<L, T> visitor, T item)
        {
            return visitor.DoStuffAsync(this, item);
        }
    }
}
