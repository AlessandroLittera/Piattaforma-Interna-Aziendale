using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Web.HatHelper
{
    public interface IResolutorFacade
    {
        Task<User> UserAsync(string email);
        Task<Assignement> CreateUserAndDefaultAccount(Assignement assignement);
    }
}
