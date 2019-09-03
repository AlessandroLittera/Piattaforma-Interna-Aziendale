using System;
using System.Collections.Generic;
using System.Text;

namespace Provider.Sql
{
    public class SqlVeicleAssignement
    {
        public int Id { get; set; }
        public virtual SqlAccount SqlAccount { get; set; }
        public virtual SqlVeicle SqlVeicle { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsValid { get; set; }
        public string Note { get; set; }
        
    }
}
