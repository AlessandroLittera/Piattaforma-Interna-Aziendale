using AutoMapper;
using Models;
using Models.AccountTypes;
using Provider.Sql.SqlAccountTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Models.RequestTypes;
using Provider.Sql.SqlRequests;

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
                .Include<Malattia,SqlMalattia>()
                .Include<Trasferta,SqlTrasferta>()
                .Include<Permesso,SqlPermesso>()
                .Include<Ferie,SqlFerie>();

            CreateMap<Malattia, SqlMalattia>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Permesso, SqlPermesso>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Trasferta, SqlTrasferta>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Ferie, SqlFerie>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<SqlRequest, Request>()
                .Include<SqlTrasferta, Trasferta>()
                .Include<SqlPermesso, Permesso>()
                .Include<SqlMalattia,Malattia>()
                .Include<SqlFerie,Ferie>();


            CreateMap<SqlFerie, Ferie>();
            CreateMap<SqlTrasferta, Trasferta>();
            CreateMap<SqlPermesso, Permesso>();
            CreateMap<SqlMalattia, Malattia>();

            CreateMap<SqlVeicle, Veicle>();
            CreateMap<Veicle,SqlVeicle>()
                .ForMember(x => x.Id, opt => opt.Ignore());













        }
    }
}
