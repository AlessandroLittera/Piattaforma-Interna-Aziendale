
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
            return mapper.Map<List<Request>>(dbcontext.SqlContexts);
        }

        public async Task<Request> CreateRequestAsync(Request context)
        {
            ObjectEmpty(context);
            var sqlContext = mapper.Map<SqlRequest>(context);
            dbcontext.SqlContexts.Add(sqlContext);
            await dbcontext.SaveChangesAsync();
            return mapper.Map<Request>(sqlContext);
        }

        public async Task<bool> DeleteAsync(Request context)
        {
            ObjectEmpty(context);
            if (int.TryParse(context.Id, out int idContext))
            {
                var sqlContext = dbcontext.SqlContexts.FirstOrDefault(x => x.Id == idContext);
                ObjectEmpty(sqlContext);
                dbcontext.Remove(sqlContext);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            throw new NullReferenceException();
        }

        public async Task<Request> EditAsync(Request context)
        {
            ObjectEmpty(context);
            if (int.TryParse(context.Id, out int idContext))
            {
                var sqlContext = dbcontext.SqlContexts.FirstOrDefault(x => x.Id == idContext);
                ObjectEmpty(sqlContext);
                mapper.Map(sqlContext, context);
                await dbcontext.SaveChangesAsync();
                return mapper.Map<Request>(sqlContext);


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

