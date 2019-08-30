using System;
using System.Collections.Generic;
using System.Text;

namespace Provider.Sql.SqlAccountTypes
{
   public abstract class SqlAccountType
    {
        public int Id { get; set; }
        public bool IsMailingList { get; set; }
        public virtual ICollection<SqlAccount> SqlAccounts { get; set; }
        public SqlAccountType()
        {
            this.SqlAccounts = new HashSet<SqlAccount>();
        }
    }
}
