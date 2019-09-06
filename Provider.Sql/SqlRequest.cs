using System;
using System.Collections.Generic;
using System.Text;

namespace Provider.Sql
{
    public class SqlRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEdit { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public virtual ICollection<SqlRequestAssignement> SqlRequestAssignements { get; set; }
       
        public SqlRequest()
        {
            this.Name = string.Empty;
            this.SqlRequestAssignements = new HashSet<SqlRequestAssignement>();
        }
    } 
}
