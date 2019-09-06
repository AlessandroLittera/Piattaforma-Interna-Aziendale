using Models.Interfaces.Visistor;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Request
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<RequestAssignement> requestAssignements { get; set; }

        public Request()
        {
            Name = string.Empty;
            Id = string.Empty;

        }

    }


}

