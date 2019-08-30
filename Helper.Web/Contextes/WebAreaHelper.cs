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
    public class WebAreaHelper : IAreaHelper
    {
        private IAreaProvider provider;
        private IAccountProvider accountProvider;
        public WebAreaHelper(IAreaProvider provider, IAccountProvider accountProvider)
        {
            this.provider = provider;
            this.accountProvider = accountProvider;
        }

        public Task<ICollection<Account>> AccountNotPresentAsync(string id)
        {
            return provider.AccountNotPresentAsync(id);
        }

        public Task<ICollection<Account>> AccountsAsync()
        {
            return provider.AccountsAsync();
        }

        public async Task<Role> AddAccountAsync(string areaId, List<string> accountsId)
        {
            Role role = new Role()
            {
                Context = await GetByIdAsync(areaId)
            };
            foreach (var acc in accountsId)
            {
                Account account = await accountProvider.GetById(acc);
                role.Account = account;
                await provider.AddAccountAsync(role);
            }
            return role;
        }

        public Task<ICollection<Area>> AreasAsync()
        {
            return provider.AreasAsync();
        }

        public async Task<Role> ChangeInChargeAsync(string areaId, string accountId)
        {
            Role role = new Role
            {
                Context = await GetByIdAsync(areaId),
                Account = await accountProvider.GetById(accountId)
            };

            return await provider.ChangeInChargeAsync(role);
        }

        public Task<Area> CreateAreaAsync(Area area, Account account)
        {
            return provider.CreateAreaAsync(area, account);
        }

        public Task<bool> DeleteAsync(Area area)
        {
            return provider.DeleteAsync(area);
        }

        public Task<Area> EditNameAsync(Area area)
        {
            return provider.EditNameAsync(area);
        }

        public Task<Area> GetByIdAsync(string id)
        {
            return provider.GetByIdAsync(id);
        }

        public Task<ICollection<Account>> ListDomainAccountsAsync()
        {
           return provider.ListDomainAccountsAsync();
        }

        public Task<bool> RemoveAccountOnArea(string roleId)
        {
            return provider.RemoveAccountOnArea(roleId);
        }

        public Task<ICollection<Role>> RolesFromArea(Area area)
        {
            return provider.RolesFromArea(area);
        }
    }
}
