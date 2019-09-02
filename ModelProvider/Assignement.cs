using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    
    public class Assignement
    {
        
        public string Id { get; set; }
     
        public User User { get; set; } = new User();

        public Account Account { get; set; } 
        public DateTime CreationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public DateTime LastEdit { get; set; }
    }
}
