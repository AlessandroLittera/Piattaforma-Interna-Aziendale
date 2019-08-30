using Models;
using Models.Contextes;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Models.Resources;

namespace Provider.Sql.SqlProviders
{
    public class SqlPossibleRoleProvider : IPossibleRoleProvider
    {
        private SqlModelsContext dbcontext;
        private IMapper mapper;
        public SqlPossibleRoleProvider(SqlModelsContext dbcontext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbcontext = dbcontext;
        }
        public async Task<PossibleRole> CreateUserAsync(PossibleRole possibleRole)
        {
            ObjectEmpty(possibleRole);
            SqlPossibleRole sqlPossibleRole = mapper.Map<SqlPossibleRole>(possibleRole);
            dbcontext.SqlPossibleRoles.Add(sqlPossibleRole);
            await dbcontext.SaveChangesAsync();
            return mapper.Map<PossibleRole>(sqlPossibleRole);

        }
        public async Task<bool> DeleteAsync(PossibleRole possibleRole)
        {

            ObjectEmpty(possibleRole);
            if (int.TryParse(possibleRole.Id, out int id))
            {
                SqlPossibleRole sqlPossible = dbcontext.SqlPossibleRoles.FirstOrDefault(x => x.Id == id);
                ObjectEmptyFromDb(sqlPossible);
                dbcontext.Remove(possibleRole);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }
        public async Task<PossibleRole> EditAsync(PossibleRole possibleRole)
        {
            ObjectEmpty(possibleRole);
            if (int.TryParse(possibleRole.Id, out int id))
            {
                SqlPossibleRole sqlPossible = dbcontext.SqlPossibleRoles.FirstOrDefault(x => x.Id == id);
                mapper.Map(sqlPossible,possibleRole);
                await dbcontext.SaveChangesAsync();
                return mapper.Map<PossibleRole>(sqlPossible);
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }
        public async Task<ICollection<PossibleRole>> PossibleRolesAsync()
        {
            await Task.Delay(0);
            return mapper.Map<List<PossibleRole>>(dbcontext.SqlPossibleRoles);
        }
        public void ObjectEmpty(Object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException(/*Resource.ObjectEmpty*/);
            }
        }

        public void ObjectEmptyFromDb(Object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException(Resource.ObjectNullFromDb);
            }
        }

        public async Task<PossibleRole> GetByIdAsync(string id)
        {
            await Task.Delay(0);
            if (int.TryParse(id, out int possibleId))
            {
                return mapper.Map<PossibleRole>(dbcontext.SqlPossibleRoles.FirstOrDefault(x=>x.Id == possibleId));
            }
            throw new NullReferenceException(Resource.InvalidOperation);
        }

        //methods for unit test
        public async Task<bool> DeletePossibleRole(ICollection<PossibleRole> possibleRoles)
        {
            if (possibleRoles == null)
            {
                throw new NullReferenceException(Resource.ObjectEmpty);
            }
            foreach (var  roles in possibleRoles)
            {
                if (int .TryParse(roles.Id, out int rolesId))
                {
                    SqlPossibleRole sqlPossibleRole = dbcontext.SqlPossibleRoles.FirstOrDefault(x=>x.Id == rolesId);
                    dbcontext.SqlPossibleRoles.Remove(sqlPossibleRole);
                    await dbcontext.SaveChangesAsync();
                    
                }
            }
            return true;
        }
    }
   
   


}
