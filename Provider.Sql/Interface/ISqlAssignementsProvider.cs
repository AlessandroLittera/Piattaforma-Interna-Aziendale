using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Sql.Interface
{
    public interface ISqlAssignementsProvider
    {
        Task<bool> SetAssignemet(SqlUser sqlUser, SqlAccount sqlAccount);
        Task<ICollection<Account>> AccountsFromUser(SqlUser sqlUser);
        Task<ICollection<User>> UsersFromAccount(SqlAccount sqlAccount);
        Task<bool> DeleteAssignement(SqlAccount sqlAccount, SqlUser sqlUser);
    }
}
