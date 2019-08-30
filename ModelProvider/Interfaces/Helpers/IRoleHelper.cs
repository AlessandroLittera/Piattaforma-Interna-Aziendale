using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Helpers
{
    public interface IRoleHelper
    {
        Task<ICollection<Role>> RolesAsync();
        Task<bool> DeleteAsync(Role role);
        Task<Role> EditAsync(Role role);
        Task<Role> CreateRoleAsync(Role role);
    }
}
