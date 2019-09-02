using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Provider.Sql.SqlAccountTypes;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Mime;
using System.Threading.Tasks;
using static Provider.Sql.SqlProviders.SqlAccountProvider;

namespace Provider.Sql
{
    public abstract class SqlAccount
    {
       
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required.")]
        public string Nickname { get; set; }
        public bool IsDefault { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate{ get; set; }
        public DateTime? DeactivationDate { get; set; }
        public DateTime LastEdit { get; set; }
        public virtual ICollection<SqlAssignement> SqlAssignements { get; set; }
       

        
       

        public SqlAccount()
        {
           
            this.SqlAssignements = new HashSet<SqlAssignement>();
            this.Nickname = string.Empty;
            this.Email = string.Empty;
            this.IsDefault = false;
            this.SqlAssignements = new HashSet<SqlAssignement>();
            


        }
    }
}
