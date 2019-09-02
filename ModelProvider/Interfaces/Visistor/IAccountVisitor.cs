using Models.AccountTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Visistor
{
    public interface IAccountVisitor<L, T>
    {
        Task<L> DoStuffAsync(Standard account, T item);
    }
}
