using System;
using System.Collections.Generic;
using System.Text;

namespace Models.AccountTypes
{
    public abstract class AccountType
    {
        public string Id { get; set; }
        public bool IsMailingList { get; set; }
        
    }
    
}
