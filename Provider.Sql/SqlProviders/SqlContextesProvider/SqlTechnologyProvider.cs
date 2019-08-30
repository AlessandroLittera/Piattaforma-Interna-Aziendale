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
    public class SqlTechnologyProvider : ITechnologyProvider
    {
        public SqlModelsContext dbcontext;
        private IMapper mapper;
        private readonly IRoleProvider roleProvider;
        public SqlTechnologyProvider(SqlModelsContext sqlModelsContext, IMapper mapper, IRoleProvider roleProvider)
        {
            this.roleProvider = roleProvider;
            this.mapper = mapper;
            this.dbcontext = sqlModelsContext;
        }

        public Task<ICollection<Account>> AccountNotPresentAsync(string id)
        {
            return roleProvider.AccountNotPresentAsync(id);
        }
        
        public Task<Role> ChangeAdminAsync(Role role)
        {
            return roleProvider.ChangeInChargeAccountsAsync(role);
        }

        public async Task<Technology> CreateTechnologyAsync(Technology technology, Account account)
        {
            if (technology == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            if (int.TryParse(account.Id, out int accountId))
            {
                SqlAccount sqlAccount = dbcontext.SqlAccounts.Where(x=>x.DeactivationDate == null).FirstOrDefault(x => x.Id == accountId);
                SqlTechnology sqlTechnology = mapper.Map<SqlTechnology>(technology);
                sqlTechnology.CreationDate = DateTime.UtcNow;
                sqlTechnology.LastEdit = DateTime.UtcNow;
                var Admin = new SqlPossibleRole()
                {
                    Name = "Admin",
                };
                sqlTechnology.PossibleRoles.Add(Admin);

                sqlTechnology.PossibleRoles.Add(new SqlPossibleRole()
                {
                    Name = "Belonging to",
                });

                sqlTechnology.Roles.Add(new SqlRole
                {
                    SqlAccount = sqlAccount,
                    SqlPossibleRole = Admin
                });
                dbcontext.SqlTechnologies.Add(sqlTechnology);
                await dbcontext.SaveChangesAsync();
                return mapper.Map<Technology>(sqlTechnology);
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }

        public async Task<bool> DeleteAsync(Technology technology)
        {
            if (technology == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            if (int.TryParse(technology.Id, out int technologyId))
            {
                SqlTechnology sqlTechnology = dbcontext.SqlTechnologies.FirstOrDefault(x=>x.Id == technologyId);

               
                if (sqlTechnology.SqlApplications == null || sqlTechnology.SqlApplications.Count() == 0)
                {
                    sqlTechnology.DeactivationDate = DateTime.UtcNow;
                    sqlTechnology.LastEdit = DateTime.UtcNow;
                    await dbcontext.SaveChangesAsync();
                    return true;
                }
               // lanciare eccezione per dire che ha ancora aree
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }

        public async Task<Technology> EditAsync(Technology technology)
        {
            if (technology == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);

            }
            if (int.TryParse(technology.Id, out int technologyId))
            {
                SqlTechnology sqlTechnology = dbcontext.SqlTechnologies.FirstOrDefault(x=>x.Id == technologyId);
                sqlTechnology.Name = technology.Name;
                await dbcontext.SaveChangesAsync();
                return mapper.Map<Technology>(sqlTechnology);
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }

        public Task<Account> GetAccountById(string id)
        {
            return roleProvider.GetAccountById(id);
        }

        public async Task<Technology> GetByIdAsync(string id)
        {
            await Task.Delay(0);
            if (id == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            if (int.TryParse(id, out int technologyId))
            {
                SqlTechnology sqlTechnology = dbcontext.SqlTechnologies.Where(x => x.DeactivationDate == null).FirstOrDefault(x => x.Id == technologyId);
                return mapper.Map<Technology>(sqlTechnology);
            }
            throw new NullReferenceException(Resource.InvalidOperation);
            
        }

        public async Task<ICollection<Account>> ListDomainAccountsAsync()
        {
            await Task.Delay(0);
            List<SqlAccount> sqlAccounts = dbcontext.SqlAccounts.Where(x => x.Email.Contains("vetrya.com")).Where(x => x.DeactivationDate == null).ToList();
            if (sqlAccounts == null)
            {
                throw new NullReferenceException(Resource.ObjectNullFromDb);
            }
            return mapper.Map<List<Account>>(sqlAccounts);
        }

        public Task<ICollection<Role>> RolesFromTechnologyAsync(Technology technology)
        {
            return roleProvider.RoleFromContextAsync(technology);
        }

        public async Task<ICollection<Technology>> TechnologysAsync()
        {
            await Task.Delay(0);
            return mapper.Map<List<Technology>>(dbcontext.SqlTechnologies.Where(x=>x.DeactivationDate == null));
        }


        // methods for xunit test

        public async Task<bool> DeleteCreatedTEchnology(string Id)
        {
            if (Id == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            if (int.TryParse(Id, out int techId))
            {
                SqlTechnology sqlTechnology = dbcontext.SqlTechnologies.FirstOrDefault(x=>x.Id == techId);
                dbcontext.SqlTechnologies.Remove(sqlTechnology);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ReviveDeletedTechnology(string Id)
        {
            if (Id == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            if (int.TryParse(Id, out int techId))
            {
                SqlTechnology sqlTechnology = dbcontext.SqlTechnologies.FirstOrDefault(x=>x.Id == techId);
                sqlTechnology.DeactivationDate = null;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> RemoveRoleCreated(ICollection<Role> roles)
        {
            if (roles == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            foreach (var role in roles)
            {
                if (int.TryParse(role.Id, out int roleId))
                {
                    SqlRole sqlRole = dbcontext.SqlRoles.FirstOrDefault(x => x.Id == roleId);
                    dbcontext.SqlRoles.Remove(sqlRole);
                    await dbcontext.SaveChangesAsync();
                   
                }

            }
            return true;
        }
    }
}
