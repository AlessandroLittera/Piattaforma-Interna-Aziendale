using AutoMapper;
using Models;
using Models.Contextes;
using Models.AccountTypes;
using Provider.Sql.SqlAccountTypes;
using Provider.Sql.SqlContextes;
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
                .ForMember(x => x.SqlRoles, opt => opt.Ignore())
                .Include<DPO, SqlDPO>()
                .Include<RSGSI, SqlRSGSI>()
                .Include<StakeHolder, SqlStakeHolder>()
                .Include<God, SqlGod>()
                .Include<Standard, SqlStandard>();


            CreateMap<DPO, SqlDPO>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<RSGSI, SqlRSGSI>()
                 .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<StakeHolder, SqlStakeHolder>()
                 .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Standard, SqlStandard>()
                 .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<God, SqlGod>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<SqlAccount, Account>()
                .Include<SqlDPO, DPO>()
                .Include<SqlGod, God>()
                .Include<SqlRSGSI, RSGSI>()
                .Include<SqlStakeHolder, StakeHolder>()
                .Include<SqlStandard, Standard>();

            CreateMap<SqlDPO, DPO>();
            CreateMap<SqlRSGSI, RSGSI>();
            CreateMap<SqlStakeHolder, StakeHolder>();
            CreateMap<SqlStandard, Standard>();
            CreateMap<SqlGod, God>();


            CreateMap<Context, SqlContext>()
                .Include<Area, SqlArea>()
                .Include<Application, SqlApplication>()
                .Include<Technology, SqlTechnology>();
            CreateMap<SqlContext, Context>()
                .Include<SqlArea, Area>()
                .Include<SqlApplication, Application>()
                .Include<SqlTechnology, Technology>();

            CreateMap<SqlRole, Role>()

               .ForMember(x => x.Account, opt => opt.MapFrom(a => a.SqlAccount))
               .ForMember(x => x.Context, opt => opt.MapFrom(c => c.SqlContext))
               .ForMember(x => x.DefaultRole, opt => opt.MapFrom(c => c.SqlPossibleRole));

            CreateMap<Role, SqlRole>()
              .ForMember(x => x.SqlAccount, opt => opt.MapFrom(a => a.Account))
               .ForMember(x => x.SqlContext, opt => opt.MapFrom(c => c.Context))
               .ForMember(x => x.SqlPossibleRole, opt => opt.MapFrom(c => c.DefaultRole));

            CreateMap<SqlArea, Area>()
                .ForMember(x => x.Applications, opt => opt.Ignore());

            CreateMap<Area, SqlArea>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<SqlApplication, Application>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Technology, opt => opt.Ignore())
                .ForMember(x => x.Area, opt => opt.Ignore());

            CreateMap<Application, SqlApplication>()
                 .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.SqlTechnology, opt => opt.Ignore())
                .ForMember(x => x.SqlArea, opt => opt.Ignore());

            CreateMap<SqlTechnology, Technology>()
                .ForMember(x => x.Applications, opt => opt.Ignore());

            CreateMap<Technology, SqlTechnology>()
              .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.SqlApplications, opt => opt.Ignore());

            CreateMap<SqlPossibleRole, PossibleRole>();
            CreateMap<PossibleRole, SqlPossibleRole>();



        }
    }
}
