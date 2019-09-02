using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Models;
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

            

            CreateMap<Account, CreateAccount>()
                .ForPath(x => x.Nickname, opt => opt.MapFrom(a => a.Nickname))
                .ForPath(x => x.Email, opt => opt.MapFrom(a => a.Email))
                .ForPath(x => x.AccountType, opt => opt.MapFrom(a => a.AccountType));

            CreateMap<CreateAccount, Account>().ConvertUsing(source => Account.GetInstanceOf(source.Id, source.AccountType, source.Email, source.Nickname));

            CreateMap<Assignement, CreateUser>();
            
            
        }
    }
    
}


