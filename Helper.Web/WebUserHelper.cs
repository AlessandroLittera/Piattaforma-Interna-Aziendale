    using Models;
using Models.Interfaces.Helpers;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Web
{
    public class WebUserHelper : IUserHelper
    {
        private IUserProvider provider;
        public WebUserHelper(IUserProvider provider)
        {
            this.provider = provider;
        }

        public Task<ICollection<Account>> AccountsAsync()
        {
            return provider.AccountsAsync();
        }

        public Task<ICollection<Assignement>> AssignementsByUserIdAsync(string id)
        {
            return provider.AssignementsByUserIdAsync(id);
        }

        public  Task<User> CreateUserAsync(User user)
        {
            return  provider.CreateUserAsync(user);
        }

        public Task<bool> DeleteAssignementAsync(string id)
        {
            return provider.DeleteAssignement(id);
        }

        public Task<bool> DeleteAsync(User user)
        {
            return provider.DeleteAsync(user);
        }

        public Task<User> EditAsync(User user)
        {
            return provider.EditAsync(user);
        }

        public Task<User> GetAsync(User user)
        {
            return provider.GetAsync(user);
        }

        public Task<ICollection<User>> GetByEmailAsync(string email)
        {
            return provider.GetByEmailAsync(email);
        }

        public async Task<Assignement> SetAssignementAsync(string userId, List<string> accountsId)
        {

            Assignement assignement = new Assignement
            {
                User = await provider.GetById(userId)
            };
            foreach (var acc in accountsId)
            {
                Account account = await provider.GetAccountById(acc);
                assignement.Account = account;
                await provider.SetAssignementAsync(assignement);
            }
            return assignement;
        }

        public Task<ICollection<User>> UsersAsync()
        {
            return provider.UsersAsync();
        }

        public Task<ICollection<Account>> AccountsNotPresentAsync(string id)
        {
            return provider.AccountsNotPresentAsync(id);
        }

        public Task<User> GetById(string id)
        {
            return provider.GetById(id);
        }
    }
}
