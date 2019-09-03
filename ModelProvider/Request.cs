using Models.Interfaces.Visistor;
using Models.RequestTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public abstract class Request
    {
        
        public string Id { get; set; }
        public string Name { get; set; }
        public abstract RequestType RequestType { get; }
        public abstract Task<L> VisitAsync<L, T>(IRequestVisitor<L, T> visitor, T item);

        public Request()
        {
            Name = string.Empty;
            Id = string.Empty;
        
        }
        public static Request GetIstanceOf(RequestType requestType)
        {
            switch (requestType)
            {
                case RequestType.Ferie:
                    return new Ferie();
                case RequestType.Malattia:
                    return new Malattia();
                case RequestType.Trasferta:
                    return new Trasferta();
                case RequestType.Permesso:
                    return new Permesso();
                default: return null;
            }
        }

        
    }

    public enum RequestType
    {
        Permesso = 0,
        Ferie = 1,
        Trasferta = 2,
        Malattia = 3,
    }
}
