using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Veicle
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<VeicleAssignement> VeicleAssignements { get; set; }

    }
}
