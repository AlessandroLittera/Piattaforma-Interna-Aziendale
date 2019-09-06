using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vap.Models
{
    public class RichiestaTrasf
    {
        
        string Id { get; set; }
        string Nome { get; set; }
        DateTime From { get; set; }
        DateTime To { get; set; }
        List<RequestAssignement> requestAss = new List<RequestAssignement>();
      

    }
}
