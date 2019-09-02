using Models;
using Models.Contextes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Providers
{
    public interface IRoleProvider
    {
        Task<ICollection<Role>> RolesAsync();
        Task<Role> CreateRoleAsync(Role role);
        Task<bool> DeleteAsync(Role role);
        Task<Role> EditAsync(Role role);
        Task<Role> ChangeInChargeAccountsAsync(Role role);
        Task<ICollection<Role>> RoleFromContextAsync(Context context);
        Task<ICollection<Account>> AccountNotPresentAsync(string id);
        Task<Account> GetAccountById(string id);

        // methods for xunitTest
        Task<bool> RemoveRoleCreated(string id);
        Task<bool> ReviveRoleDeleted(string id);
        Task<Role> GetRole(string id);
    }
} 