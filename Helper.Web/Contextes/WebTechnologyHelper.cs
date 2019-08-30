using Models;
using Models.Contextes;
using Models.Interfaces.Helpers;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Web.Contextes
{
    public class WebTechnologyHelper : ITechnologyHelper
    {
        private ITechnologyProvider provider;
        public WebTechnologyHelper(ITechnologyProvider provider)
        {
            this.provider = provider;
        }

        public Task<ICollection<Account>> AccountNotPresentAsync(string id)
        {
            return provider.AccountNotPresentAsync(id);
        }

        public async Task<Role> ChangeAdminAsync(string technologyId, string accountId)
        {
            Role role = new Role
            {
                Context = await GetByIdAsync(technologyId),
                Account = await provider.GetAccountById(accountId)
            };

            return await provider.ChangeAdminAsync(role);
        }


        public Task<Technology> CreateTechnologyAsync(Technology technology, Account account)
        {
            return provider.CreateTechnologyAsync(technology, account);
        }

        public Task<bool> DeleteAsync(Technology technology)
        {
            return provider.DeleteAsync(technology);
        }

        public Task<Technology> EditAsync(Technology technology)
        {
            return provider.EditAsync(technology);
        }

        public Task<Technology> GetByIdAsync(string id)
        {
            return provider.GetByIdAsync(id);
        }

        public Task<ICollection<Account>> ListDomainAccountsAsync()
        {

            return provider.ListDomainAccountsAsync();
        }

        public Task<ICollection<Role>> RolesFromTechnologyAsync(Technology technology)
        {
            return provider.RolesFromTechnologyAsync(technology);
        }

        public Task<ICollection<Technology>> TechnologysAsync()
        {
            return provider.TechnologysAsync();
        }
    }
}
