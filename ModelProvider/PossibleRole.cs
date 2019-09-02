using Models.Contextes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public  class PossibleRole
    {
        public string Id { get; set; }
        public string Name { get; set; } 
        public ICollection<Role> Roles { get; set; }
    }
}
