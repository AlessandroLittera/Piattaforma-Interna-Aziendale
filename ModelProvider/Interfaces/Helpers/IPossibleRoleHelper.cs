using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Helpers
{
    public interface IPossibleRoleHelper
    {
        Task<ICollection<PossibleRole>> PossibleRolesAsync();
        Task<bool> DeleteAsync(PossibleRole possibleRole);
        Task<PossibleRole> EditAsync(PossibleRole possibleRole);
        Task<PossibleRole> CreateUserAsync(PossibleRole possibleRole);

    }
}
