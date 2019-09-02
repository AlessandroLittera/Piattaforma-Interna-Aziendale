using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Contextes
{
   public abstract class Context
    {
        public virtual ICollection<PossibleRole> PossibleRoles { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

        public Context()
        {
            Name = string.Empty;
            Id = string.Empty;
            PossibleRoles = new HashSet<PossibleRole>();
            Roles = new HashSet<Role>();
        }
    }
}
