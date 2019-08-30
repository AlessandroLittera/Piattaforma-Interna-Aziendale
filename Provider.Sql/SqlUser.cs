using AutoMapper;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Provider.Sql
{
   public  class SqlUser
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Image { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public DateTime LastEdit { get; set; }
        public virtual ICollection<SqlAssignement> SqlAssignements { get; set; }
        

        public SqlUser()
        {
            this.SqlAssignements = new HashSet<SqlAssignement>();
            this.Name = string.Empty;
            this.Surname = string.Empty;
            this.DeactivationDate = null;
            this.Image = "admin";
        }
    }
}