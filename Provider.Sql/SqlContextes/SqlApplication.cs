
using System;
using System.Collections.Generic;
using System.Text;

namespace Provider.Sql.SqlContextes
{
   public class SqlApplication : SqlContext

    {
        public  virtual SqlArea SqlArea { get; set; }
        public  virtual SqlTechnology SqlTechnology { get; set; }

        public SqlApplication()
        {
            this.SqlArea = new SqlArea();
            this.SqlTechnology = new SqlTechnology();
        }
        
    }
}
