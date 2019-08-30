using Models.Contextes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Models.Interfaces.Providers
{
    public interface IAreaProvider
    {
        Task<ICollection<Area>> AreasAsync();
        Task<bool> DeleteAsync(Area area);
        Task<Area> EditNameAsync(Area area);
        Task<Area> CreateAreaAsync(Area area, Account account);
        Task<ICollection<Account>> AccountsAsync();
        Task<Area> GetByIdAsync(string id);
        Task<ICollection<Account>> ListDomainAccountsAsync();
        Task<Role> ChangeInChargeAsync(Role role);
        Task<ICollection<Role>> RolesFromArea(Area area);
        Task<ICollection<Account>> AccountNotPresentAsync(string id);
        Task<Role> AddAccountAsync(Role role);
        Task<Account> GetAccountById(string id);
        Task<bool> RemoveAccountOnArea(string roleId);

        // api for unit Test 

        Task<bool> DeleteCreatedArea(string id);
        Task<bool> ReviveDeletedArea(string id);
        Task<bool> RemoveRoleCreated(ICollection<Role> roles);

    }
}
