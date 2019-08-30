using Models.Contextes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Providers
{
   public interface IContextProvider
    {
        Task<ICollection<Context>> ContextsAsync();
        Task<bool> DeleteAsync(Context context);
        Task<Context> EditAsync(Context context);
        Task<Context> CreateContextAsync(Context context);

    }
}
