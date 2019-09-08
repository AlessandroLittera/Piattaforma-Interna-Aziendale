
using Models.Interfaces.Providers;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Provider.Sql;
using Models;
using Microsoft.EntityFrameworkCore;

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
            return mapper.Map<List<Request>>(dbcontext.SqlRequests.Where(x => x.DeactivationDate == null));
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
                sqlRequest.DeactivationDate = DateTime.UtcNow;
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
                throw new NullReferenceException();
            }
        }

        public async Task<ICollection<Request>> RequestByAccountIdAsync(string aId)
        {
            await Task.Delay(0);
            if (int.TryParse(aId, out int accountId))
            {
                List<SqlRequestAssignement> sqlRequestAssignements = dbcontext.SqlRequestAssignements.Where(x => x.SqlAccount.Id == accountId).ToList();
                List<SqlRequest> sqlRequests = new List<SqlRequest>();
                foreach (var request in sqlRequestAssignements)
                {
                    sqlRequests.Add(request.SqlRequest);
                }
                return mapper.Map<List<Request>>(sqlRequests);

            }
            throw new NullReferenceException();
        }

        public async Task<ICollection<RequestAssignement>> RequestAssignementsValidByRequestIdAsync(string id)
        {
            await Task.Delay(0);
            if (int.TryParse(id, out int requestId))
            {
                List<SqlRequestAssignement> sqlRequestAssignements = dbcontext.SqlRequestAssignements.Where(x=>x.SqlRequest.Id == requestId)
                                                                                                     .Where(x=>x.IsValid).ToList();
                return mapper.Map<List<RequestAssignement>>(sqlRequestAssignements);
            }
            return null;
        }

        public async Task<ICollection<RequestAssignement>> RequestAssignementsByRequestIdAsync(string id)
        {
            await Task.Delay(0);
            if (int.TryParse(id, out int requestId))
            {
                List<SqlRequestAssignement> sqlRequestAssignements = dbcontext.SqlRequestAssignements.Where(x => x.SqlRequest.Id == requestId).ToList();
                                                                                                 
                return mapper.Map<List<RequestAssignement>>(sqlRequestAssignements);
            }
            return null;

        }

        public async Task<bool> SaveRequestAssignementAsync(RequestAssignement requestAssignement)
        {
            if (requestAssignement == null)
            {
                if (int.TryParse(requestAssignement.Account.Id,out int accountId) & int.TryParse(requestAssignement.Request.Id, out int requestId))
                {
                    SqlAccount sqlAccount = await dbcontext.SqlAccounts.FirstOrDefaultAsync(x => x.Id == accountId);
                    SqlRequest sqlRequest = await dbcontext.SqlRequests.FirstOrDefaultAsync(x => x.Id == requestId);
                    SqlRequestAssignement sqlRequestAssignement = Mapper.Map<SqlRequestAssignement>(requestAssignement);
                    sqlRequestAssignement.SqlAccount = sqlAccount;
                    sqlRequestAssignement.SqlRequest = sqlRequest;
                    dbcontext.SqlRequestAssignements.Add(sqlRequestAssignement);
                    return await dbcontext.SaveChangesAsync() > 0;
                }
               

            }
            return false;
        }

        public async Task<Request> RetrieveByType(string type)
        {
            switch (type)
            {
                case "Malattia":
                    {
                        var req=    await dbcontext.SqlRequests.FirstOrDefaultAsync(x => x.Name.Equals("Malattia"));
                        return mapper.Map<Request>(req);
                    }
                case "Ferie":
                    {
                        var req=    await dbcontext.SqlRequests.FirstOrDefaultAsync(x => x.Name.Equals("Ferie"));
                        return mapper.Map<Request>(req);
                    }case "Trasfrerta":
                    {
                        var req=    await dbcontext.SqlRequests.FirstOrDefaultAsync(x => x.Name.Equals("Trasferta"));
                        return mapper.Map<Request>(req);
                    }case "Permesso":
                    {
                        var req=    await dbcontext.SqlRequests.FirstOrDefaultAsync(x => x.Name.Equals("Permesso"));
                        return mapper.Map<Request>(req);
                    }
                default: return null;
            };
        }
    }


}

