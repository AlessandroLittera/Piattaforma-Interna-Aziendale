﻿using Models;
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

    }
}
