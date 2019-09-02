using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models.Interfaces.Visistor;

namespace Models.AccountTypes
{
    public class Admin : Account
    {
        public override AccountantTypes AccountType => AccountantTypes.Admin;

        public override Task<L> VisitAsync<L, T>(IAccountVisitor<L, T> visitor, T item)
        {
            return visitor.DoStuffAsync(this, item);
        }
    }
}
