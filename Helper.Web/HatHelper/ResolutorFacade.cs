using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Interfaces.Helpers;
using System.Linq;


namespace Helper.Web.HatHelper
{
    public class ResolutorFacade : IResolutorFacade 
    {
        private IUserHelper userHelper;
        private IAccountHelper accountHelper;
        public ResolutorFacade(IUserHelper userHelper, IAccountHelper accountHelper)
        {
            this.userHelper = userHelper;
            this.accountHelper = accountHelper;
        }

        public async Task<Assignement> CreateUserAndDefaultAccount(Assignement assignement)
        {
            
                User user = assignement.User;
                Account account = assignement.Account;
                account.IsDefault = true;
                var fullUser = await userHelper.CreateUserAsync(user);
                var fullAccount = await accountHelper.CreateAccountAsync(account);
            List<string> accountsId = new List<string>
                {
                    fullAccount.Id
                };

            return await userHelper.SetAssignementAsync(fullUser.Id, accountsId); 
        }

        public async Task<User> UserAsync(string email)
        {
            //controllo dominio mail 
            var usersFromEmail = await userHelper.GetByEmailAsync(email);
            var assignments = usersFromEmail.SelectMany(x => x.Assignements).ToList();
            var thisIsMyAccount = assignments.Where(x => x.Account.IsDefault)
                .FirstOrDefault();
            return thisIsMyAccount.User;

            //return usersFromEmail.FirstOrDefault(x => x.Assignements.FirstOrDefault(y => y.Account.IsDefault).User);

            //User user = usersFromEmail.FirstOrDefault();
            //var assignements = await userHelper.AssignementsByUserIdAsync(user.Id);
            //List<Account> accounts = new List<Account>();
            //foreach (var acc in assignements)
            //{
            //    accounts.Add(acc.Account);
            //}
            //return accounts;
            //// per ora lo risolvo così
        }
    }
}
