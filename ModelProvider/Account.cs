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

        public Account()
        {

            Assignements = new HashSet<Assignement>();
        }
        public string Id { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public DateTime LastEdit { get; set; }
        public bool IsDefault { get; set; }
        public ICollection<Assignement> Assignements { get; set; }
        public ICollection<RequestAssignement> requestAssignements { get; set; }
        public ICollection<VeicleAssignement> veicleAssignements { get; set; }



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
                case AccountantTypes.Admin:
                    return new Admin();
               
                default:
                    return new Standard();
            }
        }


    }

    public enum AccountantTypes
    {
        Admin = 0,
        Standard= 1,
    }



}
