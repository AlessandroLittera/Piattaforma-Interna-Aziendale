using Microsoft.EntityFrameworkCore;
using Models;
using Models.Resources;
using Models.Interfaces.Providers;
using Provider.Sql.SqlProviders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using System.Resources;


namespace Provider.Sql
{
    public class SqlUserProvider : IUserProvider
    {

        private SqlModelsContext dbContext;
        private IMapper mapper;
        public SqlUserProvider(SqlModelsContext dbContext, IMapper mapper)
        {

            this.mapper = mapper;
            this.dbContext = dbContext;

        }
        public async Task<User> CreateUserAsync(User user)
        {
            ObjectEmpty(user);
            SqlUser sqlUser = new SqlUser();
            user.LastEdit = DateTime.UtcNow;
            user.CreationDate = DateTime.UtcNow;
            mapper.Map(user, sqlUser);
            dbContext.SqlUsers.Add(sqlUser);
            await dbContext.SaveChangesAsync();
            return mapper.Map<User>(sqlUser);

        }// create a user passed on params
        public async Task<bool> DeleteAsync(User user)
        {
            ObjectEmpty(user);
            if (int.TryParse(user.Id, out int id))
            {
                SqlUser sqlUser = await dbContext.SqlUsers.FirstOrDefaultAsync(x => x.Id == id);
                ObjectEmptyFromDb(sqlUser);
                if (sqlUser.DeactivationDate != null)
                {
                    throw new Exception(Resource.user_already_disabled);
                }

                sqlUser.DeactivationDate = DateTime.UtcNow;
                sqlUser.LastEdit = DateTime.UtcNow;
                foreach (var ass in sqlUser.SqlAssignements.Where(x => x.DeactivationDate == null))
                {
                    ass.DeactivationDate = DateTime.UtcNow;
                    ass.LastEdit = DateTime.UtcNow;
                }
                // mapper.Map(user, sqlUser);
                await dbContext.SaveChangesAsync();
                return true;
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        } // set deactivation date of a specific user
        public async Task<User> EditAsync(User user)
        {
            ObjectEmpty(user);
            if (int.TryParse(user.Id, out int myId))
            {
                SqlUser sqlUser = await dbContext.SqlUsers.Where(x => x.DeactivationDate == null).FirstOrDefaultAsync(x => x.Id == myId);
                ObjectEmptyFromDb(sqlUser);
                if (sqlUser.DeactivationDate == null)
                {
                    user.LastEdit = DateTime.Now;
                    mapper.Map(user,sqlUser);
                    await dbContext.SaveChangesAsync();
                    return mapper.Map<User>(sqlUser);
                }
                throw new NullReferenceException(Resource.ObjectNullFromDb);
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }  // edit a user by changes passed on params
        public async Task<ICollection<User>> UsersAsync()
        {
            await Task.Delay(0);

            return mapper.Map<List<User>>(dbContext.SqlUsers.Where(x => x.DeactivationDate == null));
        }  // return all users present on db that are enabled
        public async Task<User> GetAsync(User user)
        {
            if (int.TryParse(user.Id, out int userId))
            {
                SqlUser sqlUser = await dbContext.SqlUsers.FirstOrDefaultAsync(x => x.Id == userId);
                ObjectEmptyFromDb(sqlUser);
                if (sqlUser.DeactivationDate == null)
                {
                    return mapper.Map<User>(sqlUser);
                }
                throw new NullReferenceException(Resource.ObjectNullFromDb);
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        } // return the user passed on params
        public async Task<ICollection<User>> GetByEmailAsync(string email)
        {
            await Task.Delay(0);
            ObjectEmpty(email);
            SqlAccount sqlAccount = dbContext.SqlAccounts.Where(x => x.DeactivationDate == null).FirstOrDefault(x => x.Email == email);
            ObjectEmptyFromDb(sqlAccount);
            ICollection<SqlAssignement> sqlAssignements = dbContext.SqlAssignements.Where(x => x.SqlAccount == sqlAccount).Where(x => x.DeactivationDate == null).ToList();
            List<User> users = new List<User>();
            foreach (var user in sqlAssignements)
            {

                users.Add(mapper.Map<User>(user.SqlUser));
            }
            return users;
        } // return a collection of user by email of account
        public async Task<Assignement> SetAssignementAsync(Assignement assignement)
        {

            if (int.TryParse(assignement.User.Id, out int idUser) && int.TryParse(assignement.Account.Id, out int accountId))
            {
                var sqlAccount = dbContext.SqlAccounts.FirstOrDefault(x => x.Id == accountId);
                ObjectEmptyFromDb(sqlAccount);
                var sqlUser = dbContext.SqlUsers.FirstOrDefault(x => x.Id == idUser);
                ObjectEmptyFromDb(sqlUser);
                if (sqlAccount == null || sqlUser == null)
                {
                    throw new NullReferenceException(Resource.ObjectNullFromDb);
                }

                assignement.CreationDate = DateTime.UtcNow;
                assignement.LastEdit = DateTime.UtcNow;
                var sqlAssign = mapper.Map<SqlAssignement>(assignement);
                sqlAssign.SqlAccount = sqlAccount;
                sqlAssign.SqlUser = sqlUser;
                dbContext.SqlAssignements.Add(sqlAssign);
                await dbContext.SaveChangesAsync();
                return mapper.Map<Assignement>(sqlAssign);
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        } // create new relation between user and account
        public async Task<ICollection<Assignement>> AssignementsByUserIdAsync(string id)
        {
            if (int.TryParse(id, out int userId))
            {
                List<Assignement> listAssignement = new List<Assignement>();
                SqlUser sqlUser = await dbContext.SqlUsers.FirstOrDefaultAsync(x => x.Id == userId);
                ObjectEmptyFromDb(sqlUser);
                ICollection<SqlAssignement> sqlAssignements = dbContext.SqlAssignements.Where(x => x.SqlUser == sqlUser).Where(x => x.DeactivationDate == null).ToList();
                if (sqlAssignements == null)
                {
                    throw new NullReferenceException(Resource.ObjectNullFromDb);
                }
                foreach (var acc in sqlAssignements)
                {
                    listAssignement.Add(mapper.Map<Assignement>(acc));

                }
                return listAssignement;
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        } //return a collection of user relation 
        public async Task<bool> DeleteAssignement(string id)
        {
            if (int.TryParse(id, out int assignementId))
            {
                SqlAssignement sqlAssignement = await dbContext.SqlAssignements.FirstOrDefaultAsync(x => x.Id == assignementId);
                ObjectEmptyFromDb(sqlAssignement);
                sqlAssignement.DeactivationDate = DateTime.UtcNow;
                sqlAssignement.LastEdit = DateTime.UtcNow;
                await dbContext.SaveChangesAsync();
                return true;
            }
            throw new NullReferenceException(Resource.InvalidOperation);

        } // set deactivation date of a specific relation
        public async Task<ICollection<Account>> AccountsAsync()
        {
            await Task.Delay(0);
            return mapper.Map<List<Account>>(dbContext.SqlAccounts.Where(x => x.DeactivationDate == null));
        } // return all the accounts presents in db that are enabled
        public async Task<ICollection<Account>> AccountsNotPresentAsync(string id)
        {
            await Task.Delay(0);
            if (int.TryParse(id, out int userId))
            {
                List<SqlAccount> sqlAccounts = dbContext.SqlAccounts
                    .Where(x => x.DeactivationDate == null)
                    .Where(x => !x.IsDefault)
                    .Where(x => !x.SqlAssignements.Any(y => y.SqlUser.Id == userId))
                    .ToList();
                ObjectEmptyFromDb(sqlAccounts);
                var assignementsByUser = dbContext.SqlAssignements.Where(x => x.SqlUser.Id == userId).Where(x => x.DeactivationDate == null);

                return mapper.Map<ICollection<Account>>(sqlAccounts);
            }
            throw new NullReferenceException();
        } // return a list of account that don't have a relation with user passed on params
        public void ObjectEmpty(Object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException(/*Resource.ObjectEmpty*/);
            }
        } // check if an object passed on params are null
        public async Task<Account> GetAccountById(string id)
        {
            await Task.Delay(0);
            if (int.TryParse(id, out int accountId))
            {
                return mapper.Map<Account>(dbContext.SqlAccounts.FirstOrDefault(x => x.Id == accountId));
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        } // return an account by it's id passed on params
        public async Task<User> GetById(string id)
        {
            await Task.Delay(0);
            if (int.TryParse(id, out int userId))
            {
                return mapper.Map<User>(dbContext.SqlUsers.FirstOrDefault(x => x.Id == userId));
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        } // return an user by it's id passed on params
        public void ObjectEmptyFromDb(Object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException(Resource.ObjectNullFromDb);
            }
        }
        // methods for unitTest

        public async Task<bool> RemoveUser(User user)
        {
            if (int.TryParse(user.Id, out int userId))
            {
                var aUser = await dbContext.SqlUsers.FirstOrDefaultAsync(x => x.Id == userId);
                dbContext.Remove(aUser);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> ResetUserAsync(User user)
        {
            if (int.TryParse(user.Id, out int userId))
            {
                SqlUser sqlUser = await dbContext.SqlUsers.FirstOrDefaultAsync(x => x.Id == userId);
                sqlUser.DeactivationDate = null;
                foreach (var assignement in sqlUser.SqlAssignements)
                {
                    assignement.DeactivationDate = null;
                    await dbContext.SaveChangesAsync();
                }
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> ResetAccountAsync(Account account)
        {
            if (int.TryParse(account.Id, out int accountId))
            {
                SqlAccount sqlAccount = await dbContext.SqlAccounts.FirstOrDefaultAsync(x => x.Id == accountId);
                sqlAccount.DeactivationDate = null;
                foreach (var assignement in sqlAccount.SqlAssignements)
                {
                    assignement.DeactivationDate = null;
                    await dbContext.SaveChangesAsync();
                }

                return true;
            }
            return false;
        }
        public async Task<bool> ResetAssignementAsync(Assignement assignement)
        {

            if (int.TryParse(assignement.Id, out int assignementId))
            {
                SqlAssignement sqlAssignement = await dbContext.SqlAssignements.FirstOrDefaultAsync(x => x.Id == assignementId);
                sqlAssignement.DeactivationDate = null;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }
        public async Task<bool> RemoveAccount(Account account)
        {
            if (int.TryParse(account.Id, out int accountId))
            {
                var aAccount = await dbContext.SqlAccounts.FirstOrDefaultAsync(x => x.Id == accountId);
                dbContext.Remove(aAccount);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> RemoveAssignement(Assignement assignement)
        {
            if (int.TryParse(assignement.Id, out int assignementId))
            {
                var aAssignement = await dbContext.SqlAssignements.FirstOrDefaultAsync(x => x.Id == assignementId);
                dbContext.Remove(aAssignement);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }


}
