using System;
using System.Collections.Generic;
using System.Text;

namespace Provider.Sql
{
   public class SqlRichiesta
    {
        public int Id { get; set; }
        public RequestTypes Type { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public virtual SqlAccount Richidente { get; set; }
    }

    public enum RequestTypes
    {
        Ferie = 0,
        Permesso = 1,
        Malattia = 2,
        Trasferta = 3,
        Straordinari = 4,

        
    }
}
