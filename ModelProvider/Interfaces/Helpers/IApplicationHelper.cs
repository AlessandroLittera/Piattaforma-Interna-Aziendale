using Models.Contextes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Helpers
{
    public interface IApplicationHelper
    {
        Task<ICollection<Application>> ApplicationsAsync();
        Task<bool> DeleteAsync(Application application);
        Task<Application> EditAsync(Application application);
        Task<Application> CreateApplicationAsync(Application application);

    }
}
