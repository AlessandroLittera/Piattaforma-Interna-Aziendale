using Models;
using Models.Contextes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vap.Models
{
    public class ListAccountsForTechnology
    {
        public string Id { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public Technology technology { get; set; }
        public ListAccountsForTechnology()
        {
            this.Accounts = new HashSet<Account>();
            this.technology = new Technology();
            this.Id = string.Empty;

        }
    }
}
