using Models;
using Models.Contextes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vap.Models
{
    public class ListAccountsForArea
    {
        public string Id { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public Area area { get; set; }
        public ListAccountsForArea()
        {
            this.Accounts = new HashSet<Account>();
            this.area = new Area();
            this.Id = string.Empty;

        }
    }
}
