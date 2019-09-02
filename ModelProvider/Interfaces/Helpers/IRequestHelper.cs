using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Helpers
{
    public interface IRequesttHelper
    {
        Task<ICollection<Request>> RequestsAsync();
        Task<bool> DeleteAsync(Request request);
        Task<Request> EditAsync(Request request);
        Task<Request> CreateContextAsync(Request request);

    }
}
