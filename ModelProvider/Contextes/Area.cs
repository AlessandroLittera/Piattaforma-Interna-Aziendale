using Models.Contextes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Contextes
{
  public class Area: Context
    {
        public Area(): base()
        {
            this.Applications = new HashSet<Application>();
        }
        public  ICollection<Application> Applications { get; set; }
    }
}
