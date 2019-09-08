using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Providers
{
   public interface IRequestProvider
    {
        Task<ICollection<Request>> RequestsAsync();
        Task<bool> DeleteAsync(Request request);
        Task<Request> EditAsync(Request request);
        Task<Request> CreateRequestAsync(Request request);
        Task<ICollection<Request>> RequestByAccountIdAsync(string accountId);
        Task<ICollection<RequestAssignement>> RequestAssignementsValidByRequestIdAsync(string id);
        Task<ICollection<RequestAssignement>> RequestAssignementsByRequestIdAsync(string id);
        Task<bool> SaveRequestAssignementAsync(RequestAssignement requestAssignement);
        Task<Request> RetrieveByType(string type);
        Task<Request> GetById(string id);
        Task<ICollection<RequestAssignement>> RequestAssignementsAsync();
        Task<ICollection<RequestAssignement>> RequestAssignementsToValidateAsync();

    }
}
