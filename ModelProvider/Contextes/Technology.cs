using Models.Contextes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Contextes
{
    public class Technology :Context
    {
        public Technology(){

            this.Applications = new HashSet<Application>();
        }
        public ICollection<Application> Applications { get; set; }
    }
}
