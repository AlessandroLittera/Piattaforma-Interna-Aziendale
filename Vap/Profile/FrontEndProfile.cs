using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Models;
using Models.Contextes;
using Vap.Models;

namespace Vap.Profile
{
    public class FrontEndProfile : AutoMapper.Profile
    {
        public FrontEndProfile()
        {
            CreateMap<CreateUser, Assignement>()
                //.ForMember(x => x.Account, opt => opt.Ignore())
                //.ForMember(x => x.User, opt => opt.Ignore())
                .ConvertUsing(source => new Assignement()
                {
                    Account = Account.GetInstanceOf(source.Id, source.AccountType, source.Email, source.Nickname),
                    User = new User()
                    {
                        Name = source.Name,
                        Surname = source.Surname,
                        Image = source.Image
                    }
                });

            CreateMap<CreateArea, FrontArea>();
            CreateMap<FrontArea, CreateArea>();

            CreateMap<Account, CreateAccount>()
                .ForPath(x => x.Nickname, opt => opt.MapFrom(a => a.Nickname))
                .ForPath(x => x.Email, opt => opt.MapFrom(a => a.Email))
                .ForPath(x => x.AccountType, opt => opt.MapFrom(a => a.AccountType));

            CreateMap<CreateAccount, Account>().ConvertUsing(source => Account.GetInstanceOf(source.Id, source.AccountType, source.Email, source.Nickname));

            CreateMap<Assignement, CreateUser>();
            CreateMap<FrontArea, Area>();
            CreateMap<Area, FrontArea>()
                .ConvertUsing<AreaConverter>();

            CreateMap<FrontTechnologie, Technology>();
            CreateMap<Technology, FrontTechnologie>()
                .ConvertUsing<TecnologyConverter>();

        }
    }
    class TecnologyConverter : ITypeConverter<Technology, FrontTechnologie>
    {
        public FrontTechnologie Convert(Technology source, FrontTechnologie destination, ResolutionContext context)
        {

            destination = new FrontTechnologie();
            if (source == null)
            {
                return destination;
            }
            var possibleRoles = source.PossibleRoles.ToList();
            var adminRole = possibleRoles.FirstOrDefault(x => x.Name == "Admin");
            var nrole = source.Roles.Where(x => x.DeactivationDate == null);
            var validRoles = source.Roles.Where(x => x.DeactivationDate == null);
            var roles = validRoles.FirstOrDefault(x => x.DefaultRole.Id == adminRole.Id);
            var account = roles.Account;

            destination.Name = source.Name;
            destination.Id = source.Id;
            destination.InCharge = account.Email;
            destination.Roles = source.Roles;

            return destination;
        }
    }

    class AreaConverter : ITypeConverter<Area, FrontArea>
    {
        public FrontArea Convert(Area source, FrontArea destination, ResolutionContext context)
        {

            destination = new FrontArea();
            if (source == null)
            {
                return destination;
            }
            var possibleRoles = source.PossibleRoles.ToList();
            var adminRole = possibleRoles.FirstOrDefault(x => x.Name == "In charge");
            var nrole = source.Roles.Where(x=>x.DeactivationDate == null);
            var validRoles = source.Roles.Where(x=>x.DeactivationDate == null);
            var roles = validRoles.FirstOrDefault(x => x.DefaultRole.Id == adminRole.Id);
            var account = roles.Account;

            destination.Name = source.Name;
            destination.Id = source.Id;
            destination.InCharge = account.Email;
            destination.Naccounts = source.Roles.Where(x => x.DeactivationDate == null).Count();
            destination.Roles = source.Roles;

            return destination;
        }
    }
}


