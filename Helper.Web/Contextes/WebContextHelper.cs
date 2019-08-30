using Models.Contextes;
using Models.Interfaces.Helpers;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Web.Contextes
{
    public class WebContextHelper : IContextHelper
    {
        private IContextProvider provider;
        public WebContextHelper(IContextProvider provider)
        {
            this.provider = provider;
        }

        public Task<ICollection<Context>> ContextsAsync()
        {
            return provider.ContextsAsync();
        }

        public Task<Context> CreateContextAsync(Context context)
        {
            return provider.CreateContextAsync(context);
        }

        public Task<bool> DeleteAsync(Context context)
        {
            return provider.DeleteAsync(context);
        }

        public Task<Context> EditAsync(Context context)
        {
            return provider.EditAsync(context);
        }
    }
}
