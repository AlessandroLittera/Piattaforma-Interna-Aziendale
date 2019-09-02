using Models;
using Models.Interfaces.Helpers;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Web
{
    public class WebRoleHelper : IRoleHelper
    {
        private IRoleProvider provider;
        public WebRoleHelper(IRoleProvider provider)
        {
            this.provider = provider;
        }
        public Task<Role> CreateRoleAsync(Role role)
        {
            return provider.CreateRoleAsync(role);
        }

        public Task<bool> DeleteAsync(Role role)
        {
            return provider.DeleteAsync(role);
        }

        public Task<Role> EditAsync(Role role)
        {
            return provider.EditAsync(role);
        }

        public Task<ICollection<Role>> RolesAsync()
        {
            return provider.RolesAsync();
        }
    }
}
