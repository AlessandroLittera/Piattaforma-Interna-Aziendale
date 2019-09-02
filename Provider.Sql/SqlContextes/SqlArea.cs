using Provider.Sql.SqlContextes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Provider.Sql.SqlContextes
{ 

    public class SqlArea : SqlContext
    {
        public virtual ICollection<SqlApplication> SqlApplications { get; set; }

        public SqlArea()
        {
            this.SqlApplications = new HashSet<SqlApplication>();
        }
    }
}
