using AutoMapper;
using Models;
using Models.AccountTypes;
using Provider.Sql.SqlAccountTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Provider.Sql
{
    public class SqlProfile : Profile
    {
        public SqlProfile()
        {
            CreateMap<User, SqlUser>()
                .ForMember(x => x.SqlAssignements, opt =>opt.Ignore());

            CreateMap<SqlUser, User>()
               .ForMember(x => x.Assignements, opt => opt.MapFrom(a => a.SqlAssignements.Where(x => x.DeactivationDate == null)));


            CreateMap<SqlAssignement, Assignement>()
                .ForPath(a => a.Account, opt => opt.MapFrom(a => a.SqlAccount))
                .ForPath(a => a.User, opt => opt.MapFrom(a => a.SqlUser));

            CreateMap<Assignement, SqlAssignement>()
                .ForPath(a => a.SqlAccount, opt => opt.MapFrom(a => a.Account))
                .ForPath(a => a.SqlUser, opt => opt.MapFrom(a => a.User));


            CreateMap<Account, SqlAccount>()
               
                .Include<Standard, SqlStandard>();


            
            CreateMap<Standard, SqlStandard>()
                 .ForMember(x => x.Id, opt => opt.Ignore());
           

            CreateMap<SqlAccount, Account>()
              
                .Include<SqlStandard, Standard>();

           
            CreateMap<SqlStandard, Standard>();



            CreateMap<Request, SqlRequest>();

            CreateMap<SqlRequest, Request>();
                

           
          

         

           

            

           

           


        }
    }
}
