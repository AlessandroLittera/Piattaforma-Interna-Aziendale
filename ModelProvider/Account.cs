using Models.Interfaces.Visistor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Models.AccountTypes;

namespace Models
{
    public abstract class Account
    {
        public const string God = "God";

        public Account()
        {
            this.Roles = new HashSet<Role>();
            Assignements = new HashSet<Assignement>();
        }
        public string Id { get; set; }

        [Required]
        public string Nickname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required.")]
        public bool IsMailingList { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public DateTime LastEdit { get; set; }
        public bool IsDefault { get; set; }
        public ICollection<Assignement> Assignements { get; set; }


        public ICollection<Role> Roles { get; set; }

        [Required]
        public abstract AccountantTypes AccountType { get; }

        public abstract Task<L> VisitAsync<L, T>(IAccountVisitor<L, T> visitor, T item);

        public static Account GetInstanceOf(string Id, AccountantTypes type, string email, string nickname)
        {
            var account = GetInstanceOf(type);
            account.Id = Id;
            account.Email = email;
            account.Nickname = nickname;
            return account;
        }

        public static Account GetInstanceOf(AccountantTypes type)
        {
            switch (type)
            {
                case AccountantTypes.DPO:
                    return new DPO();
                case AccountantTypes.RSGSI:
                    return new RSGSI();
                case AccountantTypes.Stakeholder:
                    return new StakeHolder();
                case AccountantTypes.God:
                    return new God();
                default:
                    return new Standard();
            }
        }


    }

    public enum AccountantTypes
    {
        DPO = 0,
        Stakeholder = 1,
        Standard = 2,
        RSGSI = 3,
        God = 4
    }



}
