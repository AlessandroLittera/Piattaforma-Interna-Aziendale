using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Providers
{
    public interface IAccountProvider
    {
        Task<ICollection<Account>> AccountsAsync();
        Task<bool> DeleteAsync(string id);
        Task<Account> EditAsync(Account account);
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> GetById(string id);
        Task<Account> GetByEmailAsync(string email);
        Task<ICollection<Account>> GetByUserAsync(User user);
        Task<ICollection<Assignement>> AssignementsbyAccountIdAsync(string id);
        Task<ICollection<RequestAssignement>> RequestAssignementsValidByAccountIdAsync(string id);
        Task<ICollection<RequestAssignement>> RequestAssignementsByAccountIdAsync(string Id);
        Task<ICollection<VeicleAssignement>> VeicleAssignementValidByAccountIdAsync(string id);
        Task<ICollection<VeicleAssignement>> VeicleAssignementByAccountIdAsync(string id);
        Task<ICollection<User>> UsersAsync();
        Task<bool> DeleteAssignement(string id);
        Task<Assignement> SetAssignementAsync(Assignement assignement);
        Task<ICollection<User>> UsersNotPresentAsync(string id);
        Task<User> GetUserById(string id);
        Task<User> CheckUser(string email, string password);
        Task<bool> ChangePassword(Account account);

        // for unitTest
        Task<bool> RemoveUser(User user);
        Task<bool> RemoveAccount(Account account);
        Task<bool> RemoveAssignement(Assignement assignement);
        Task<bool> ResetUserAsync(User user);
        Task<bool> ResetAccountAsync(Account account);
        Task<bool> ResetAssignementAsync(Assignement assignement);
    }
}
