using Models;
using Models.Interfaces.Helpers;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Web
{
    public class WebAccountHelper : IAccountHelper
    {
        private IAccountProvider provider;
        public  WebAccountHelper(IAccountProvider provider)
        {
            this.provider = provider;
        } 
        public Task<ICollection<Account>> AccountsAsync()
        {
            return  provider.AccountsAsync();
        }

        public Task<ICollection<User>> UsersNotPresentAsync(string id)
        {
            return provider.UsersNotPresentAsync(id);
        }

        public Task<ICollection<Assignement>> AssignementsbyAccountIdAsync(string id)
        {
            return provider.AssignementsbyAccountIdAsync(id);
        }

        public Task<Account> CreateAccountAsync(Account account)
        {
            return  provider.CreateAccountAsync(account);
        }

        public Task<bool> DeleteAssignement(string id)
        {
            return provider.DeleteAssignement(id);
        }

        public Task<bool> DeleteAsync(string id)
        {
            return provider.DeleteAsync(id);
        }

        public Task<Account> EditAsync(Account account)
        {
            return  provider.EditAsync(account);
        }
        public Task<Account> GetByEmailAsync(string email)
        {
            return provider.GetByEmailAsync(email);
        }

        public Task<Account> GetById(string id)
        {
            return provider.GetById(id);
        }

        public Task<ICollection<Account>> GetByUserAsync(User user)
        {
            return provider.GetByUserAsync(user);
        }
        public async Task<Assignement> SetAssignementAsync(string accountId, List<string> usersId)
        {
            Assignement assignement = new Assignement
            {
                Account = await provider.GetById(accountId)
            };

            foreach (var us in usersId)
            {
                assignement.User = await provider.GetUserById(us);

                 await provider.SetAssignementAsync(assignement);
            }
            return assignement;
        }
        

        public Task<ICollection<User>> UsersAsync()
        {
            return provider.UsersAsync();
        }

        public Task<User> CheckUser(string email, string password)
        {
            return provider.CheckUser(email, password);
        }

        public Task<ICollection<RequestAssignement>> RequestAssignementsValidByAccountIdAsync(string id)
        {
            return provider.RequestAssignementsValidByAccountIdAsync(id);
        }

        public Task<ICollection<RequestAssignement>> RequestAssignementsByAccountIdAsync(string Id)
        {

            return provider.RequestAssignementsByAccountIdAsync(Id);
        }

        public Task<ICollection<VeicleAssignement>> VeicleAssignementValidByAccountIdAsync(string id)
        {
            return provider.VeicleAssignementValidByAccountIdAsync(id);
        }

        public Task<ICollection<VeicleAssignement>> VeicleAssignementByAccountIdAsync(string id)
        {
            return provider.VeicleAssignementByAccountIdAsync(id);
        }
    }
}
