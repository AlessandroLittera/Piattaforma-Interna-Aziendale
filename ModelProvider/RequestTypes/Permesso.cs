using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models.Interfaces.Visistor;

namespace Models.RequestTypes
{
    public class Permesso : Request
    {
        public override RequestType RequestType => RequestType.Permesso;

        public override Task<L> VisitAsync<L, T>(IRequestVisitor<L, T> visitor, T item)
        {
            return visitor.DoStuffAsync(this, item);
        }
    }
}
