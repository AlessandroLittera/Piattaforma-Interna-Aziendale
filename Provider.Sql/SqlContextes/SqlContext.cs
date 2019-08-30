using System;
using System.Collections.Generic;
using System.Text;

namespace Provider.Sql.SqlContextes
{
    public abstract class SqlContext
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SqlRole> Roles { get; set; }
        public virtual ICollection<SqlPossibleRole> PossibleRoles { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEdit { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public SqlContext()
        {
            this.PossibleRoles = new HashSet<SqlPossibleRole>();
            this.Roles = new HashSet<SqlRole>();
                
        }
    } 
}
