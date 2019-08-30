using Models.AccountTypes;
using Models.Interfaces.Visistor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.AccountTypes
{
    public class Standard : Account
    {
        public override AccountantTypes AccountType => AccountantTypes.Standard;

        public override Task<L> VisitAsync<L, T>(IAccountVisitor<L, T> visitor, T item)
        {
            return visitor.DoStuffAsync(this, item);
        }
    }
}
