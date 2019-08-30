using Models;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Models.Resources;
using Models.Contextes;
using Provider.Sql.SqlContextes;

namespace Provider.Sql.SqlProviders
{
    public class SqlRoleProvider : IRoleProvider
    {
        private SqlModelsContext dbcontext;
        private IMapper mapper;
        public SqlRoleProvider(SqlModelsContext modelsContext, IMapper mapper )
        {
            this.mapper = mapper;
            this.dbcontext = modelsContext;
        }
       
        public async Task<Role> CreateRoleAsync(Role role)
        {
            await Task.Delay(0);
            if(role == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            if (int.TryParse(role.Account.Id, out int accountId)&&int.TryParse(role.Context.Id, out int contextId)&& int.TryParse(role.DefaultRole.Id, out int possibleRoleId))
            {
                SqlAccount sqlAccount = dbcontext.SqlAccounts.FirstOrDefault(x=>x.Id == accountId);
                SqlContext sqlContext = dbcontext.SqlContexts.FirstOrDefault(x => x.Id == contextId);
                SqlPossibleRole sqlPossibleRole = dbcontext.SqlPossibleRoles.FirstOrDefault(x => x.Id ==  possibleRoleId);
                SqlRole sqlRole = mapper.Map<SqlRole>(role);
                sqlRole.CreationDate = DateTime.UtcNow;
                sqlRole.LastEdit = DateTime.UtcNow;
                sqlRole.SqlAccount = sqlAccount;
                sqlRole.SqlContext = sqlContext;
                sqlRole.SqlPossibleRole = sqlPossibleRole; 
                dbcontext.Add(sqlRole);
                await dbcontext.SaveChangesAsync();
                return mapper.Map<Role>(sqlRole);
            }
            throw new NullReferenceException(Resource.InvalidOperation);
            
        }
        public async Task<bool> DeleteAsync(Role role)
        {
            ObjectEmpty(role);

            if (int.TryParse(role.Id, out int id))
            {
                SqlRole sqlRole = dbcontext.SqlRoles.FirstOrDefault(x => x.Id == id);
                ObjectEmptyFromDb(sqlRole);
                sqlRole.DeactivationDate = DateTime.UtcNow;
                sqlRole.LastEdit = DateTime.UtcNow;
                await dbcontext.SaveChangesAsync();
                return true;

            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }
        public async Task<Role> EditAsync(Role role)
        {
            ObjectEmpty(role);
            if (int.TryParse(role.Id, out int id))
            {
                SqlRole sqlRole = dbcontext.SqlRoles.FirstOrDefault(x => x.Id == id);
                ObjectEmptyFromDb(sqlRole);
                mapper.Map(sqlRole,role);
                await dbcontext.SaveChangesAsync();

                return mapper.Map<Role>(sqlRole);

            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }
        public async Task<ICollection<Role>> RolesAsync()
        {
            await Task.Delay(0);
            return mapper.Map<List<Role>>(dbcontext.SqlRoles.Where(x=>x.DeactivationDate == null));

        }
        public void ObjectEmpty(Object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
        }
        public void ObjectEmptyFromDb(Object obj)
        {
            if(obj == null)
            {
                throw new NullReferenceException(Resource.ObjectNullFromDb);
            }
        }

       

        public async Task<Role> ChangeInChargeAccountsAsync(Role role)
        {

            if (role == null )
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            if (int.TryParse(role.Context.Id, out int areaId))
            {

                SqlRole oldRole = dbcontext.SqlRoles.Where(x => x.DeactivationDate == null).Where(p =>p.SqlPossibleRole.Name != "Belonging to").FirstOrDefault(x=>x.SqlContext.Id ==  areaId) ;
                if (oldRole == null)
                {
                    throw new NullReferenceException(Resource.ObjectNullFromDb);
                }
                SqlAccount sqlAccount = oldRole.SqlAccount;
                role.DefaultRole = mapper.Map<PossibleRole>(oldRole.SqlPossibleRole);
                var sqlPossibleRoles = dbcontext.SqlPossibleRoles.Where(x => x.SqlContext.Id == areaId);
                if (int.TryParse(role.Account.Id, out int accountId))
                {
                    if (sqlAccount.Id != accountId)
                    {
                       var newRole =  await CreateRoleAsync(role);
                        await DeleteAsync(mapper.Map<Role>(oldRole));
                        return newRole;

                    }

                }
                    
            }
          
           
            throw new NullReferenceException(Resource.InvalidOperation);
        }
        public async Task<ICollection<Role>> RoleFromContextAsync(Context context)
        {
            await Task.Delay(0);
            if (context == null)
            {
                throw new NullReferenceException(Resource.InvalidOperation);
            }
            if (int.TryParse(context.Id, out int contextId))
            {
                List<SqlRole> sqlRoles = dbcontext.SqlRoles.Where(x => x.SqlContext.Id == contextId).Where(x => x.DeactivationDate == null).ToList();
                return mapper.Map<List<Role>>(sqlRoles);
                 
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }
        public async Task<ICollection<Account>> AccountNotPresentAsync(string id)
        {
            await Task.Delay(0);
            if (id == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            if (int.TryParse(id, out int contextId))
            {
                List<SqlAccount> sqlAccounts = dbcontext.SqlAccounts.Where(x=>x.DeactivationDate == null).ToList();
                var rolesFromContex = dbcontext.SqlRoles.Where(x=>x.SqlContext.Id == contextId).Where(x=>x.DeactivationDate == null);
                foreach (var roles in rolesFromContex)
                {
                    if (sqlAccounts.Contains(roles.SqlAccount))
                    {
                        sqlAccounts.Remove(roles.SqlAccount);
                    }
                }
                return mapper.Map<List<Account>>(sqlAccounts);
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }
        public async Task<Account> GetAccountById(string id)
        {
            await Task.Delay(0);
            if (int.TryParse(id, out int accountId))
            {
                return mapper.Map<Account>(dbcontext.SqlAccounts.Where(x=>x.DeactivationDate == null).FirstOrDefault(x => x.Id == accountId));
            }
            throw new NullReferenceException(Resource.InvalidOperation);

        }

        // methods for xunit test
        public async  Task<bool> RemoveRoleCreated(string id)
        {
            await Task.Delay(0);
            if (id == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            if (int.TryParse(id, out int roleId))
            {
                SqlRole sqlRole = dbcontext.SqlRoles.FirstOrDefault(x=>x.Id == roleId);
                dbcontext.SqlRoles.Remove(sqlRole);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }

        public async Task<bool> ReviveRoleDeleted(string id)
        {
            await Task.Delay(0);
            if (id == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            if (int.TryParse(id, out int roleId))
            {
                SqlRole sqlRole = dbcontext.SqlRoles.Where(x=>x.DeactivationDate != null).FirstOrDefault(x=>x.Id == roleId);
                sqlRole.DeactivationDate = null;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }

        public async  Task<Role> GetRole(string id)
        {

            await Task.Delay(0);
            if (id == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            if (int.TryParse(id, out int roleId))
            {
                return mapper.Map<Role>(dbcontext.SqlRoles.FirstOrDefault(x => x.Id == roleId));
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }
    }
}
