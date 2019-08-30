using Models.Contextes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Providers
{
    public interface ITechnologyProvider
    {
        Task<ICollection<Technology>> TechnologysAsync();
        Task<bool> DeleteAsync(Technology technology);
        Task<Technology> EditAsync(Technology technology);
        Task<Technology> CreateTechnologyAsync(Technology technology, Account account);
        Task<Technology> GetByIdAsync(string id);
        Task<ICollection<Account>> ListDomainAccountsAsync();
        Task<Role> ChangeAdminAsync(Role role);
        Task<ICollection<Role>> RolesFromTechnologyAsync(Technology technology);
        Task<ICollection<Account>> AccountNotPresentAsync(string id);
        Task<Account> GetAccountById(string id);

        // methods for unit test
        Task<bool> DeleteCreatedTEchnology(string id);
        Task<bool> ReviveDeletedTechnology(string id);
        Task<bool> RemoveRoleCreated(ICollection<Role> roles);

    }
}
