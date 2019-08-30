using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Provider.Sql
{
    public class SqlAssignement
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public virtual SqlUser SqlUser { get; set; }
        [Required(AllowEmptyStrings = false)]
        public virtual SqlAccount SqlAccount { get; set;} 
        public DateTime CreationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public DateTime LastEdit { get; set; }

    }
}
