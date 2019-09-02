using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vap.Models
{
    public class CreateArea
    {
        public string Id { get; set; }
        public ICollection<Account> Accounts{ get; set; }

        public CreateArea()
        {
            this.Accounts = new HashSet<Account>();
        }
    }
}
