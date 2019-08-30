using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vap.Models
{
    public class ListAccountsForUser
    {
        public string Id { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public User user { get; set; }
        public ListAccountsForUser()
        {
            this.Accounts = new HashSet<Account>();
            this.user = new User();
            this.Id = string.Empty;

        }

    }
}
