using Models.Contextes;
using Models.Interfaces.Helpers;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Web.Contextes
{
    public class WebApplicationHelper : IApplicationHelper
    {
        private IApplicationProvider provider;
        public WebApplicationHelper(IApplicationProvider provider)
        {
            this.provider = provider;
        }
        public Task<ICollection<Application>> ApplicationsAsync()
        {
            return provider.ApplicationsAsync();
        }

        public Task<Application> CreateApplicationAsync(Application application)
        {
            return provider.CreateApplicationAsync(application);
        }

        public Task<bool> DeleteAsync(Application application)
        {
             return provider.DeleteAsync(application);
        }

        public Task<Application> EditAsync(Application application)
        {
             return provider.EditAsync(application); 
        }
    }
}
