using Models.RequestTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Visistor
{
    public interface IRequestVisitor<L,T>
    {
        Task<L> DoStuffAsync( Ferie ferie, T item);
        Task<L> DoStuffAsync( Permesso permesso, T item);
        Task<L> DoStuffAsync( Malattia malattia, T item);
        Task<L> DoStuffAsync( Trasferta trasferta, T item);
    }
}
