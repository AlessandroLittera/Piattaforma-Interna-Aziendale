using Models.Contextes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Contextes
{
    public class Application :Context
    {
        public Area Area { get; set; }
        public Technology Technology { get; set; }

        public Application()
        {
            this.Area = null;
            this.Technology = null;
        }
    }
}
