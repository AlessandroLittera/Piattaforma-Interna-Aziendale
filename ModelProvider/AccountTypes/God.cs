
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Interfaces.Visistor;

namespace Models.AccountTypes
{
    public class God : Account
    {
        public override AccountantTypes AccountType => AccountantTypes.God;

        public override Task<L> VisitAsync<L, T>(IAccountVisitor<L, T> visitor, T item)
        {
            return visitor.DoStuffAsync(this , item);
        }
    }
}