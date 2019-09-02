using System;
using System.Collections.Generic;
using System.Text;

namespace Provider.Sql.SqlContextes
{
   public class SqlTechnology : SqlContext

    {
        public virtual ICollection<SqlApplication> SqlApplications { get; set; }

        public SqlTechnology()
        {
            this.SqlApplications = new HashSet<SqlApplication>();
        }
    }
}
