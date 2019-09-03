using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class VeicleAssignement
    {
        public string Id { get; set; }
        public Account Account { get; set; }
        public Veicle Veicle { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Note { get; set; }
        public bool IsValid { get; set; }
    }
}
