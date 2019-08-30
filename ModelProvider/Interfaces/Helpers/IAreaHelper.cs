using Models.Contextes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Helpers
{
    public interface IAreaHelper
    {
        Task<ICollection<Area>> AreasAsync();
        Task<bool> DeleteAsync(Area area);
        Task<Area> EditNameAsync(Area area);
        Task<Area> CreateAreaAsync(Area area,Account account);
        Task<ICollection<Account>> AccountsAsync();
        Task<Area> GetByIdAsync(string id);
        Task<ICollection<Account>> ListDomainAccountsAsync();
        Task<Role> ChangeInChargeAsync(string areaId, string accountId);
        Task<ICollection<Role>> RolesFromArea(Area area);
        Task<ICollection<Account>> AccountNotPresentAsync(string id);
        Task<Role> AddAccountAsync(string areaId, List<string> accountsId);
        Task<bool> RemoveAccountOnArea(string roleId);

    }
}
