using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vap.Models
{
    public class ListUsersForAccount
    {
        public string Id { get; set;}
        public ICollection<User> Users { get; set;}
        public Account account { get; set; }
        public ListUsersForAccount(){
            this.Users = new HashSet<User>();
            this.account = Account.GetInstanceOf(AccountantTypes.Standard);
            this.Id = string.Empty;

        }
    }
}
