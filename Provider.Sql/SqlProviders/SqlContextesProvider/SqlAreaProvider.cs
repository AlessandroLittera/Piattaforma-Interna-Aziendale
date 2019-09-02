using AutoMapper;
using Models;
using Models.Contextes;
using Models.Interfaces.Providers;
using Models.Resources;
using Provider.Sql.SqlContextes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Sql.SqlProviders.SqlContextesProvider
{
    public class SqlAreaProvider : IAreaProvider
    {
        private readonly IMapper mapper;
        private SqlModelsContext dbContext;
        private readonly IRoleProvider roleProvider;
        private readonly IAccountProvider accountProvider;
        public SqlAreaProvider(SqlModelsContext sqlModelsContext, IMapper mapper, IRoleProvider roleProvider, IAccountProvider accountProvider)
        {
            this.roleProvider = roleProvider;
            this.dbContext = sqlModelsContext;
            this.mapper = mapper;
            this.accountProvider = accountProvider;
        }
        public async Task<ICollection<Area>> AreasAsync()
        {
            await Task.Delay(0);
            var sqlList = dbContext.SqlAreas.Where(x => x.DeactivationDate == null).ToList();
            if (sqlList == null)
            {
                throw new NullReferenceException(Resource.ObjectNullFromDb);
            }

            List<Area> aList = mapper.Map<List<Area>>(sqlList);
            return aList;
        }

        public async Task<Area> CreateAreaAsync(Area area, Account account)
        {
            if (area == null)
            {

                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            if (int.TryParse(account.Id, out int accountId))
            {
                SqlAccount sqlAccount = dbContext.SqlAccounts.FirstOrDefault(x => x.Id == accountId);
                SqlArea sqlArea = mapper.Map<SqlArea>(area);
                sqlArea.CreationDate = DateTime.UtcNow;
                sqlArea.LastEdit = DateTime.UtcNow;

                var inChargeRole = new SqlPossibleRole()
                {
                    Name = "In charge",
                };
                sqlArea.PossibleRoles.Add(inChargeRole);
                    
                sqlArea.PossibleRoles.Add(new SqlPossibleRole()
                {
                    Name = "Belonging to",
                });

                sqlArea.Roles.Add(new SqlRole
                {
                    SqlAccount = sqlAccount,
                    SqlPossibleRole = inChargeRole
                });

                dbContext.SqlContexts.Add(sqlArea);
                await dbContext.SaveChangesAsync();


                return mapper.Map<Area>(sqlArea);
            }

            throw new NullReferenceException(Resource.InvalidOperation);
        }// funziona ma da modificare

        public async Task<bool> DeleteAsync(Area area)
        {
            if (area == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            if (int.TryParse(area.Id, out int id))
            {
                SqlArea sqlArea = dbContext.SqlAreas.FirstOrDefault(x => x.Id == id);
                sqlArea.LastEdit = DateTime.UtcNow;
                sqlArea.DeactivationDate = DateTime.UtcNow;

                foreach (var sqlRole in sqlArea.Roles.Where(x=>x.DeactivationDate == null))
                {
                    await roleProvider.DeleteAsync(mapper.Map<Role>(sqlRole));
                }
                await dbContext.SaveChangesAsync();
                return true;
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }//da verificare

        public async Task<Area> EditNameAsync(Area area)
        {
            if (area == null)
            {
                throw new NullReferenceException("Params not found");

            }
            if (int.TryParse(area.Id, out int id))
            {
                var sqlAreas = dbContext.SqlAreas.Where(x => x.DeactivationDate == null);
                SqlArea sqlArea = sqlAreas.FirstOrDefault(x => x.Id == id);
                if (sqlArea == null)
                {
                    throw new NullReferenceException(Resource.ObjectNullFromDb);
                }
                sqlArea.Name = area.Name;
                await dbContext.SaveChangesAsync();
                return area;
            }
            throw new NotImplementedException();
        }//da verificare

        public async Task<ICollection<Account>> AccountsAsync()
        {
            await Task.Delay(0);

            var myList = dbContext.SqlAccounts.Where(x => x.DeactivationDate == null).ToList();
            if (myList == null || myList.Count() == 0)
            {
                return new List<Account>();
            }

            return mapper.Map<List<Account>>(myList);
        }//verificato

        public  Task<Role> ChangeInChargeAsync(Role role)
        {
            return roleProvider.ChangeInChargeAccountsAsync(role);
        }
        public async Task<Area> GetByIdAsync(string id) 
        {
            await Task.Delay(0);
            if (id == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            if (int.TryParse(id, out int areaId))
            {
                SqlArea sqlArea = dbContext.SqlAreas.FirstOrDefault(x => x.Id == areaId);
                return mapper.Map<Area>(sqlArea);
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }
        public async Task<ICollection<Account>> ListDomainAccountsAsync()
        {
            await Task.Delay(0);
            List<SqlAccount> sqlAccounts = dbContext.SqlAccounts.Where(x=>x.Email.Contains("vetrya.com")).Where(x=>x.DeactivationDate == null).ToList();
            if (sqlAccounts == null)
            {
                throw new NullReferenceException(Resource.ObjectNullFromDb);
            }
            return mapper.Map<List<Account>>(sqlAccounts);
        }

        public Task<ICollection<Role>> RolesFromArea(Area area)
        {
            return roleProvider.RoleFromContextAsync(area);
        }

        public Task<ICollection<Account>> AccountNotPresentAsync(string id)
        {
            return roleProvider.AccountNotPresentAsync(id);
        }
        public Task<Role> AddAccountAsync(Role role)
        {
            if (int.TryParse(role.Context.Id, out int contextId))
            {
                var sqlPossibleRoles = dbContext.SqlPossibleRoles.Where(x => x.SqlContext.Id == contextId);
                SqlPossibleRole sqlPossibleRole = sqlPossibleRoles.FirstOrDefault(x => x.Name == "Belonging to");
                role.DefaultRole = mapper.Map<PossibleRole>(sqlPossibleRole);
                return roleProvider.CreateRoleAsync(role);
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }


        public Task<bool> RemoveAccountOnArea(string roleId)
        {
            return roleProvider.DeleteAsync(new Role {Id = roleId });
        }

        public Task<Account> GetAccountById(string id)
        {
            throw new NotImplementedException();
        }


        // api for xunit Test 

        public async Task<bool> DeleteCreatedArea(string Id)
        {
            await Task.Delay(0);
            if (Id == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            if (int.TryParse(Id, out int areaId))
            {
                SqlArea sqlArea = dbContext.SqlAreas.FirstOrDefault(x=>x.Id == areaId);
                dbContext.Remove(sqlArea);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ReviveDeletedArea(string Id)
        {
            await Task.Delay(0);
            if (Id == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            if (int.TryParse(Id, out int areaId))
            {
                SqlArea sqlArea = dbContext.SqlAreas.FirstOrDefault(x => x.Id == areaId);
                if (sqlArea == null)
                {
                    throw new NullReferenceException(Resource.ObjectEmpty);
                }
                var sqlRoles = dbContext.SqlRoles.Where(x=>x.SqlContext.Id == sqlArea.Id);
                foreach (var role in sqlRoles)
                {
                    role.DeactivationDate = null;
                }
                sqlArea.DeactivationDate = null;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveRoleCreated(ICollection<Role> roles)
        {
            await Task.Delay(0);
            if (roles == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            foreach (var role in roles)
            {
                if (int.TryParse(role.Id, out int roleId))
                {
                    SqlRole sqlRole = dbContext.SqlRoles.FirstOrDefault(x => x.Id == roleId);
                    dbContext.SqlRoles.Remove(sqlRole);
                    await dbContext.SaveChangesAsync();
                   
                }
                
            }
            return true;
        }




    }
}
