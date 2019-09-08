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
               .Include<Admin, SqlAdmin>()
                .Include<Standard, SqlStandard>();


            CreateMap<Admin,SqlAdmin>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Standard, SqlStandard>()
                 .ForMember(x => x.Id, opt => opt.Ignore());
           

            CreateMap<SqlAccount, Account>()
              .Include<SqlAdmin,Admin>()
                .Include<SqlStandard, Standard>();

            CreateMap<SqlAdmin, Admin>();
            CreateMap<SqlStandard, Standard>();



            CreateMap<Request, SqlRequest>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<SqlRequest, Request>();
            

            CreateMap<SqlVeicle, Veicle>();
            CreateMap<Veicle,SqlVeicle>()
                .ForMember(x => x.Id, opt => opt.Ignore());


            CreateMap<SqlVeicleAssignement, VeicleAssignement>()
                .ForMember(x => x.Veicle, opt => opt.MapFrom(src => src.SqlVeicle))
                .ForMember(x => x.Account, opt => opt.MapFrom(src => src.SqlAccount));

            CreateMap<VeicleAssignement,SqlVeicleAssignement>()
                 .ForMember(x => x.SqlVeicle, opt => opt.MapFrom(src => src.Veicle))
                 .ForMember(x => x.SqlAccount, opt => opt.MapFrom(src => src.Account));


            CreateMap<SqlRequestAssignement, RequestAssignement>()
                .ForMember(x => x.Request, opt => opt.MapFrom(src => src.SqlRequest))
                .ForMember(x => x.Account, opt => opt.MapFrom(src => src.SqlAccount));
            CreateMap<RequestAssignement, SqlRequestAssignement>()
                 .ForMember(x => x.SqlRequest, opt => opt.MapFrom(src => src.Request))
                .ForMember(x => x.SqlAccount, opt => opt.MapFrom(src => src.Account));









        }
    }
}
