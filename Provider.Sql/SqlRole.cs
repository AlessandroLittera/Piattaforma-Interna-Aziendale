using Provider.Sql.SqlContextes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Provider.Sql
{
   public  class SqlRole
    {
        public int Id { get; set; }
        public virtual SqlContext SqlContext { get; set; }
        public  virtual SqlAccount SqlAccount { get; set; }
        public virtual  SqlPossibleRole SqlPossibleRole { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public DateTime LastEdit { get; set; }

        public SqlRole()
        {
            this.SqlContext = null;
            this.SqlPossibleRole = new SqlPossibleRole();
            this.CreationDate = DateTime.UtcNow;
            this.LastEdit = DateTime.UtcNow;
            this.DeactivationDate = null;
        }
    }
}
