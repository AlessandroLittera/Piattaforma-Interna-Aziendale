using System;
using System.Collections.Generic;
using System.Text;

namespace Provider.Sql
{
    public class SqlVeicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SqlVeicleAssignement> SqlVeiclesAssignements { get; set; }
    }
}
