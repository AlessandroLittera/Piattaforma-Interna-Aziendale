
using Models.Interfaces.Providers;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Provider.Sql;
using Models;

namespace Provider.Sql.SqlProviders.SqlContextesProvider
{
    public class SqlRequestProvider : IRequestProvider
    {
        private SqlModelsContext dbcontext;
        private IMapper mapper;
        public SqlRequestProvider(SqlModelsContext sqlModelsContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbcontext = sqlModelsContext;
        }
        public async Task<ICollection<Request>> RequestsAsync()
        {
            await Task.Delay(0);
            return mapper.Map<List<Request>>(dbcontext.SqlRequests);
        }

        public async Task<Request> CreateRequestAsync(Request request)
        {
            ObjectEmpty(request);
            var sqlRequest = mapper.Map<SqlRequest>(request);
            dbcontext.SqlRequests.Add(sqlRequest);
            await dbcontext.SaveChangesAsync();
            return mapper.Map<Request>(sqlRequest);
        }

        public async Task<bool> DeleteAsync(Request request)
        {
            ObjectEmpty(request);
            if (int.TryParse(request.Id, out int idRequest))
            {
                var sqlRequest = dbcontext.SqlRequests.FirstOrDefault(x => x.Id == idRequest);
                ObjectEmpty(sqlRequest);
                dbcontext.Remove(sqlRequest);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            throw new NullReferenceException();
        }

        public async Task<Request> EditAsync(Request request)
        {
            ObjectEmpty(request);
            if (int.TryParse(request.Id, out int idRequest))
            {
                var sqlRequest = dbcontext.SqlRequests.FirstOrDefault(x => x.Id == idRequest);
                ObjectEmpty(sqlRequest);
                mapper.Map(sqlRequest, request);
                await dbcontext.SaveChangesAsync();
                return mapper.Map<Request>(sqlRequest);


            }
            throw new NullReferenceException();
        }
        public void ObjectEmpty(Object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException(/*Resource.ObjectEmpty*/);
            }
        }
    }


}

