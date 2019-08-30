using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Providers
{
   public interface IPossibleRoleProvider
    {
        Task<ICollection<PossibleRole>> PossibleRolesAsync();
        Task<bool> DeleteAsync(PossibleRole possibleRole);
        Task<PossibleRole> EditAsync(PossibleRole possibleRole);
        Task<PossibleRole> CreateUserAsync(PossibleRole possibleRole);
        Task<PossibleRole> GetByIdAsync(string id);

        // methods for unit test
        Task<bool> DeletePossibleRole(ICollection<PossibleRole> possibleRoles);
    }
}
