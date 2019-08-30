using Models;
using Models.Interfaces.Helpers;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Web
{
    public class WebPossibleRoleHelper : IPossibleRoleHelper
    {
        private IPossibleRoleProvider provider;
        public WebPossibleRoleHelper(IPossibleRoleProvider provider)
        {
            this.provider = provider;
        }
        public Task<PossibleRole> CreateUserAsync(PossibleRole possibleRole)
        {
            return provider.CreateUserAsync(possibleRole);
        }

        public Task<bool> DeleteAsync(PossibleRole possibleRole)
        {
            return provider.DeleteAsync(possibleRole);
        }

        public Task<PossibleRole> EditAsync(PossibleRole possibleRole)
        {
            return  provider.EditAsync(possibleRole);
        }

        public Task<ICollection<PossibleRole>> PossibleRolesAsync()
        {
            return  provider.PossibleRolesAsync();
        }
    }
}
