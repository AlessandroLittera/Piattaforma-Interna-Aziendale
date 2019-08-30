using Models.Contextes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Helpers
{
   public interface ITechnologyHelper
    {
        Task<ICollection<Technology>> TechnologysAsync();
        Task<bool> DeleteAsync(Technology technology);
        Task<Technology> EditAsync(Technology technology);
        Task<Technology> CreateTechnologyAsync(Technology technology,Account account);
        Task<Technology> GetByIdAsync(string id);
        Task<ICollection<Account>> ListDomainAccountsAsync();
        Task<Role> ChangeAdminAsync (string areaId, string accountId);
        Task<ICollection<Role>> RolesFromTechnologyAsync(Technology technology);
        Task<ICollection<Account>> AccountNotPresentAsync(string id);

    }
}
