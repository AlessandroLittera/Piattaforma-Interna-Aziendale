using AutoMapper;
using Models;
using Models.Resources;
using Provider.Sql.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Sql.SqlProviders
{
    public class SqlAssignementsProvider : ISqlAssignementsProvider
    {
        private SqlModelsContext dbContext;
        private IMapper mapper;
        public SqlAssignementsProvider(SqlModelsContext sqlModelsContext, IMapper mapper)
        {
            this.dbContext = sqlModelsContext;
            this.mapper = mapper;
        }

        public async Task<ICollection<Account>> AccountsFromUser(SqlUser sqlUser)
        {
            await Task.Delay(0);
            if (sqlUser == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }

            List<Account> accounts = new List<Account>();
            List<SqlAssignement> sqlAssignement = dbContext.SqlAssignements.Where(x => x.SqlUser.Id == sqlUser.Id).ToList();
            foreach (var assignement in sqlAssignement.Where(x => x.DeactivationDate == null))
            {
                accounts.Add(mapper.Map<Account>(assignement.SqlAccount));

            }
            return accounts;
        }

        public async Task<bool> DeleteAssignement(SqlAccount sqlAccount, SqlUser sqlUser)
        {
            if (sqlAccount == null || sqlUser == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            SqlAssignement sqlAssignement = dbContext.SqlAssignements.Where(x => x.SqlAccount.Id == sqlAccount.Id)
                                                                     .Where(x => x.SqlUser.Id == sqlUser.Id)
                                                                     .Where(x => x.DeactivationDate == null)
                                                                     .FirstOrDefault();
            sqlAssignement.DeactivationDate = DateTime.UtcNow;
            sqlAssignement.LastEdit = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            return true;

        }

        public async Task<bool> SetAssignemet(SqlUser sqlUser, SqlAccount sqlAccount)
        {
            if (sqlUser == null || sqlAccount == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            SqlAssignement sqlAssignement = new SqlAssignement
            {
                SqlUser = sqlUser,
                SqlAccount = sqlAccount,
                CreationDate = DateTime.UtcNow,
                LastEdit = DateTime.UtcNow
            };
            dbContext.SqlAssignements.Add(sqlAssignement);
            await dbContext.SaveChangesAsync();
            return true;


        }

        public async Task<ICollection<User>> UsersFromAccount(SqlAccount sqlAccount)
        {
            await Task.Delay(0);
            if (sqlAccount == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            List<User> users = new List<User>();
            List<SqlAssignement> sqlAssignements = dbContext.SqlAssignements.Where(x => x.SqlAccount.Id == sqlAccount.Id).ToList();
            foreach (var assignement in sqlAssignements.Where(x => x.DeactivationDate == null))
            {
                users.Add(mapper.Map<User>(assignement.SqlUser));
            }
            return users;

        }
    }
}


