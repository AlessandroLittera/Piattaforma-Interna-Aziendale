using Provider.Sql.SqlContextes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Provider.Sql
{
   public  class SqlPossibleRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual SqlContext SqlContext { get; set; }
        public virtual ICollection<SqlRole> SqlRoles { get; set; }
        public SqlPossibleRole()
        {
            this.SqlRoles = new HashSet<SqlRole>();
            this.Name = string.Empty;
            this.SqlContext = null;


        }
    }
}
