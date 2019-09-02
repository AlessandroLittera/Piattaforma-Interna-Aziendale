using Models.Contextes;
using Models.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Provider.Sql.SqlContextes;

namespace Provider.Sql.SqlProviders.SqlContextesProvider
{
    public class SqlApplicationProvider : IApplicationProvider
    {
        
        private SqlModelsContext dbcontext;
       // private ToolClass tool;
        public SqlApplicationProvider(SqlModelsContext sqlModelsContext)
        {
           // this.tool = toolClass;
            this.dbcontext = sqlModelsContext;
        }
        public async Task<ICollection<Application>> ApplicationsAsync()
        {
            await Task.Delay(0);
            var applicationContext = dbcontext.SqlContexts.Where(x=> x is SqlApplication);
            var  areacontext = dbcontext.SqlContexts.Where(x=> x is SqlArea);
            var returnList = new List<Application>();
            foreach (var application in applicationContext)
            {
                returnList.Add(new Application
                {
                    Name = application.Name,
                    Area = new Area(),
                    Technology = new Technology(),
                });
                return returnList;
            }
            

            throw new NotImplementedException();
        }

        public async Task<Application> CreateApplicationAsync(Application application)
        {
            if (application == null)
            {
                throw new NullReferenceException("Params not found");
            }
            SqlApplication sqlApplication = new SqlApplication
            {
                Name = application.Name,
                
            };
            SqlArea sqlArea = new SqlArea
            {
                Id = Convert.ToInt32(application.Area.Id),
                Name = application.Area.Name,
                
            };
            SqlTechnology sqlTechnology = new SqlTechnology
            {
                Id = Convert.ToInt32(application.Technology.Id),
                Name = application.Technology.Name,

            };
            sqlApplication.SqlArea = sqlArea;
            sqlApplication.SqlTechnology = sqlTechnology;
            dbcontext.SqlApplications.Add(sqlApplication);
            await dbcontext.SaveChangesAsync();
            application.Id = sqlApplication.Id.ToString();
            return application;
        }

        public async  Task<bool> DeleteAsync(Application application)
        {
            if (application == null)
            {
                throw new NullReferenceException("Params not found");

            }
            if (int.TryParse(application.Id,out int id))
            {
                var app = dbcontext.SqlContexts.Where(x => x is SqlContextes.SqlApplication).Where(x => x.Id == Convert.ToInt32(application.Id)); 
                if(app == null)
                {
                    throw new NullReferenceException(" Application not found");
                }
                dbcontext.Remove(application);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            throw new NotImplementedException();
        }

        public async Task<Application> EditAsync(Application application)
        {
            if (application == null)
            {
                throw new NullReferenceException("Params not found");

            }
            if (int.TryParse(application.Id, out int id))
            {
                SqlContext sqlcontext=  dbcontext.SqlContexts.Where(x => x is SqlApplication)
                                                             .FirstOrDefault(x=>x.Id == Convert.ToInt32(application.Id));
                if (sqlcontext == null)
                {
                    throw new NullReferenceException(" Application not found");
                }
                sqlcontext.Name = application.Name;

                await dbcontext.SaveChangesAsync();
                return application;
            }
            throw new NotImplementedException();
        }
    }
}
