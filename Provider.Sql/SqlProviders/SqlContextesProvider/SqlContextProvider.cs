using Models.Contextes;
using Models.Interfaces.Providers;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Provider.Sql.SqlContextes;

namespace Provider.Sql.SqlProviders.SqlContextesProvider
{
    public class SqlContextProvider : IContextProvider
    {
        private SqlModelsContext dbcontext;
        private IMapper mapper;
        public SqlContextProvider(SqlModelsContext sqlModelsContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbcontext = sqlModelsContext;
        }
        public async Task<ICollection<Context>> ContextsAsync()
        {
            await Task.Delay(0);
            return mapper.Map<List<Context>>(dbcontext.SqlContexts);
        }

        public async Task<Context> CreateContextAsync(Context context)
        {
            ObjectEmpty(context);
            var sqlContext = mapper.Map<SqlContext>(context);
            dbcontext.SqlContexts.Add(sqlContext);
            await dbcontext.SaveChangesAsync();
            return mapper.Map<Context>(sqlContext);
        }

        public async Task<bool> DeleteAsync(Context context)
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

        public async Task<Context> EditAsync(Context context)
        {
            ObjectEmpty(context);
            if (int.TryParse(context.Id, out int idContext))
            {
                var sqlContext = dbcontext.SqlContexts.FirstOrDefault(x => x.Id == idContext);
                ObjectEmpty(sqlContext);
                mapper.Map(sqlContext, context);
                await dbcontext.SaveChangesAsync();
                return mapper.Map<Context>(sqlContext);


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

