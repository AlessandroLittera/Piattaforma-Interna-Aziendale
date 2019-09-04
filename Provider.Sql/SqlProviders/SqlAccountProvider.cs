using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.AccountTypes;
using Models.Interfaces.Providers;
using Models.Interfaces.Visistor;
using Models.Resources;
using Provider.Sql.SqlAccountTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Sql.SqlProviders
{
    public class SqlAccountProvider : IAccountProvider
    {
        private SqlModelsContext dbContext;
        private IMapper mapper;
        public SqlAccountProvider(SqlModelsContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbContext = context;
        }
        public async Task<ICollection<Account>> AccountsAsync()
        {
            await Task.Delay(0);
            return mapper.Map<List<Account>>(dbContext.SqlAccounts.Where(x => x.DeactivationDate == null));

        } // return a list of accounts 
        public async Task<Account> CreateAccountAsync(Account account)
        {
            ObjectEmpty(account);
            account.CreationDate = DateTime.Now;
            account.LastEdit = DateTime.Now;
            SqlAccount sqlAccount = mapper.Map<SqlAccount>(account);
            dbContext.SqlAccounts.Add(sqlAccount);
            await dbContext.SaveChangesAsync();
            return mapper.Map<Account>(sqlAccount);

        } // create a new istance of an account
        public async Task<bool> DeleteAsync(string id)
        {
            ObjectEmpty(id);
            if (int.TryParse(id, out int accountId))
            {
                SqlAccount sqlAccount = dbContext.SqlAccounts.FirstOrDefault(x => x.Id == accountId);
                ObjectEmptyFromDb(sqlAccount);
                if (sqlAccount.DeactivationDate == null)
                {
                    sqlAccount.DeactivationDate = DateTime.Now;
                    sqlAccount.LastEdit = DateTime.Now;
                    foreach (var ass in dbContext.SqlAssignements.Where(x => x.SqlAccount.Id == accountId))
                    {
                        ass.DeactivationDate = DateTime.UtcNow;
                        ass.LastEdit = DateTime.UtcNow;
                    }

                    await dbContext.SaveChangesAsync();
                    return true;
                }

            }
            throw new NullReferenceException(Resource.InvalidOperation);
        } // set deactivationdate with current datetime
        public async Task<Account> EditAsync(Account account)
        {
            ObjectEmpty(account);
            if (int.TryParse(account.Id, out int id))
            {

                SqlAccount sqlAccount = await dbContext.SqlAccounts.FirstOrDefaultAsync(x => x.Id == id);
                ObjectEmptyFromDb(sqlAccount);
                var oldAccount = mapper.Map<Account>(sqlAccount);
                if (sqlAccount.DeactivationDate == null)
                {
                    if(oldAccount.GetType() != account.GetType())
                    {
                        account.Email = sqlAccount.Email;
                        account.Nickname = sqlAccount.Nickname;
                        account.CreationDate = account.CreationDate;
                        account.IsDefault = sqlAccount.IsDefault;
                        return await ChangeTypeAccount(account);
                    }

                    account.LastEdit = DateTime.Now;
                    mapper.Map(account, sqlAccount);
                    await dbContext.SaveChangesAsync();
                    return mapper.Map<Account>(sqlAccount);
                }
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        } // set values of object passed on params with new values
        public async Task<Account> GetByEmailAsync(string email)
        {
            ObjectEmpty(email);
            var account = await dbContext.SqlAccounts.FirstOrDefaultAsync(x => x.Email == email);
            return mapper.Map<Account>(account);

        } // return the account by it's email passed on params
        public async Task<ICollection<Account>> GetByUserAsync(User user)
        {
            ObjectEmpty(user);
            await Task.Delay(0);
            if (int.TryParse(user.Id, out int userId))
            {
                List<SqlAssignement> sqlAssignements = dbContext.SqlAssignements.Where(x => x.SqlUser.Id == userId).ToList();
                ObjectEmptyFromDb(sqlAssignements);
                List<Account> accounts = new List<Account>();
                foreach (var acc in sqlAssignements.Where(x => x.DeactivationDate == null))
                {
                    accounts.Add(mapper.Map<Account>(acc.SqlAccount));
                }
                return mapper.Map<ICollection<Account>>(accounts);
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        } // return a list of account by an user that has a relations 
        public async Task<ICollection<Assignement>> AssignementsbyAccountIdAsync(string id)
        {
            ObjectEmpty(id);
            if (int.TryParse(id, out int accountId))
            {
                List<Assignement> listAssignement = new List<Assignement>();
                SqlAccount sqlAccount = await dbContext.SqlAccounts.FirstOrDefaultAsync(x => x.Id == accountId);
                ObjectEmptyFromDb(sqlAccount);
                List<SqlAssignement> sqlAssignements = dbContext.SqlAssignements.Where(x => x.SqlAccount == sqlAccount).Where(x => x.DeactivationDate == null).ToList();
                return mapper.Map<List<Assignement>>(sqlAssignements);
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        } // return a list of relations from an account passed on params
        public async Task<ICollection<User>> UsersAsync()
        {
            await Task.Delay(0);
            return mapper.Map<List<User>>(dbContext.SqlUsers);
        }  // return a list of users that have deactivationdate setted on null
        public async Task<Account> GetById(string id)
        {
            ObjectEmpty(id);
            if (int.TryParse(id, out int accountId))
            {
                SqlAccount sqlAccount = await dbContext.SqlAccounts.FirstOrDefaultAsync(x => x.Id == accountId);
                ObjectEmptyFromDb(sqlAccount);
                return mapper.Map<Account>(sqlAccount);
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        } // return a user from it's id
        public async Task<bool> DeleteAssignement(string id)
        {
            ObjectEmpty(id);
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
        } // set deactivation date of a relation from it's id
        public async Task<Assignement> SetAssignementAsync(Assignement assignement)
        {
            ObjectEmpty(assignement);
            if (int.TryParse(assignement.User.Id, out int idUser) && int.TryParse(assignement.Account.Id, out int accountId))
            {
                var sqlAccount = dbContext.SqlAccounts.FirstOrDefault(x => x.Id == accountId);
                ObjectEmptyFromDb(sqlAccount);
                var sqlUser = dbContext.SqlUsers.FirstOrDefault(x => x.Id == idUser);
                ObjectEmptyFromDb(sqlUser);

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
        } // create a new relation passed on params
        public async Task<Account> ChangeTypeAccount(Account account)
        {
            
            //salvare la lista degli user con cui un account ha una referenza
            if (int.TryParse(account.Id, out int accountId))
            {
                List<SqlAssignement> sqlAssignements = dbContext.SqlAssignements.Where(x => x.SqlAccount.Id == accountId).ToList();
                ObjectEmptyFromDb(sqlAssignements);
                List<SqlUser> sqlUsers = new List<SqlUser>();
                Assignement assa = new Assignement();
                foreach (var ass in sqlAssignements)
                {
                    sqlUsers.Add(ass.SqlUser);
                    await DeleteAssignement(ass.Id.ToString());

                }// fatto questo dovrei avere la lista degli user salvata e eliminare le referenze tra user e account
                await DeleteAsync(account.Id);// elimino l'account
                var acc = await CreateAccountAsync(account);//creo il nuovo account

                foreach (var user in sqlUsers)
                {
                    assa.User = mapper.Map<User>(user);
                    await dbContext.SaveChangesAsync();
                    assa.Account = acc;
                    await dbContext.SaveChangesAsync();
                    await SetAssignementAsync(assa);
                }
                return account;
                // da verificare tutti i metodi che fanno, specie come procedono delete, create, set assignement e tutto il processo
                //fare un bel debug per tutti, anche staticamente


            }
            throw new NullReferenceException();
        } // allow to change type of accont with different call at delete and create method
        public async Task<ICollection<User>> UsersNotPresentAsync(string id)
        {
            await Task.Delay(0);
            ObjectEmpty(id);
            if (int.TryParse(id, out int accountId))
            {
                //fare matching per vedere che gli account non siano presenti nella relazione con lo user in assignement
                List<SqlUser> sqlUsers = dbContext.SqlUsers.Where(x => x.DeactivationDate == null).ToList();
                ObjectEmptyFromDb(sqlUsers);
                var assignementsByAccount = dbContext.SqlAssignements.Where(x => x.SqlAccount.Id == accountId).Where(x => x.DeactivationDate == null);
                foreach (var ass in assignementsByAccount)
                {
                    if (sqlUsers.Contains(ass.SqlUser))
                    {
                        sqlUsers.Remove(ass.SqlUser);
                    }
                }
                return mapper.Map<ICollection<User>>(sqlUsers);

            }
            throw new NullReferenceException(Resource.InvalidOperation);
        } // return a list of users tha haven't a relation with the account identified by id 
        public async Task<User> GetUserById(string id)
        {
            await Task.Delay(0);
            ObjectEmpty(id);
            if (int.TryParse(id, out int userId))
            {
                return mapper.Map<User>(dbContext.SqlUsers.FirstOrDefault(x => x.Id == userId));

            }
            throw new NullReferenceException(Resource.InvalidOperation);
        } // return an user from it's id

       

        public void ObjectEmpty(Object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
        }
        public void ObjectEmptyFromDb(Object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException(Resource.ObjectNullFromDb);
            }
        }

        public async Task<User> CheckUser(string email, string password)
        {
            if (email != null && password != null)
            {
                SqlAccount sqlAccount = await dbContext.SqlAccounts.Where(x => x.Email.Equals(email)).FirstOrDefaultAsync(x=>x.Password.Equals(password));
                SqlAssignement sqlAssignement = await dbContext.SqlAssignements.FirstOrDefaultAsync(x=>x.SqlAccount.Id == sqlAccount.Id);
                SqlUser sqlUser = sqlAssignement.SqlUser;
                return mapper.Map<User>(sqlUser);
            }
            throw new NullReferenceException();
        }
    }
}