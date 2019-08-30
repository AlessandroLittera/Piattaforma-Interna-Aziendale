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
        Task<L> DoStuffAsync(DPO account, T item);
        Task<L> DoStuffAsync(God account, T item);
        Task<L> DoStuffAsync(RSGSI account, T item);
        Task<L> DoStuffAsync(StakeHolder account, T item);
    }
}
