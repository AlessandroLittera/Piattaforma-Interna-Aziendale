using Models.Contextes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Role
    {

        public string Id { get; set; }
        public Context Context { get; set; }
        public Account Account { get; set; }
        public PossibleRole DefaultRole { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public DateTime LastEdit { get; set; }
    }
}
