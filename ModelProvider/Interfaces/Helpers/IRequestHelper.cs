﻿using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Helpers
{
    public interface IRequestHelper
    {
        Task<ICollection<Request>> RequestsAsync();
        Task<bool> DeleteAsync(Request request);
        Task<Request> EditAsync(Request request);
        Task<Request> CreateRequestAsync(Request request);
        Task<ICollection<Request>> RequestByAccountIdAsync(string accountId);
        Task<ICollection<RequestAssignement>> RequestAssignementsValidByRequestIdAsync(string id);
        Task<ICollection<RequestAssignement>> RequestAssignementsByRequestIdAsync(string id);
        Task<bool> SaveRequestAssignement(RequestAssignement requestAssignement); 

    }
}
