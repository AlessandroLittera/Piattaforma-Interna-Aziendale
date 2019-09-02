using Models;
using Models.Interfaces.Helpers;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Web.Contextes
{
    public class WebContextHelper : IRequesttHelper
    {
        private IRequestProvider provider;
        public WebContextHelper(IRequestProvider provider)
        {
            this.provider = provider;
        }

        public Task<ICollection<Request>> RequestsAsync()
        {
            return provider.RequestsAsync();
        }

        public Task<Request> CreateContextAsync(Request context)
        {
            return provider.CreateRequestAsync(context);
        }

        public Task<bool> DeleteAsync(Request context)
        {
            return provider.DeleteAsync(context);
        }

        public Task<Request> EditAsync(Request context)
        {
            return provider.EditAsync(context);
        }
    }
}
