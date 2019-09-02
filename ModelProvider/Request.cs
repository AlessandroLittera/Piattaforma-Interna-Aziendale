using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public abstract class Request
    {
        
        public string Id { get; set; }
        public string Name { get; set; }


        public Request()
        {
            Name = string.Empty;
            Id = string.Empty;
        
        }
    }
}
