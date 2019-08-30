using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Helpers
{
    public interface IUserHelper
    {
        Task<ICollection<User>> UsersAsync();
        Task<bool> DeleteAsync(User user);
        Task<User> EditAsync(User user);
        Task<User> CreateUserAsync(User user);
        Task<User> GetAsync(User user);
        Task<ICollection<User>> GetByEmailAsync(string email);
        Task<Assignement> SetAssignementAsync(string accountId, List<string> usersId);
        Task<ICollection<Assignement>> AssignementsByUserIdAsync(string id);
        Task<bool> DeleteAssignementAsync(string id);
        Task<ICollection<Account>> AccountsAsync();
        Task<ICollection<Account>> AccountsNotPresentAsync(string id);
        Task<User> GetById(string id);




    }
}
