using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vap.Models
{
    public class Richieste
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AccountId { get; set; }
        [Required]
        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        public DateTime To { get; set; }
        [Required]
        public string Note { get; set; }
        public string RequestType { get; set; }
        List<RequestAssignement> ListRichieste = new List<RequestAssignement>();

        

        public enum RequestTypes
        {
            Malattia = 0,
            Ferie = 1,
            Trasferta = 2,
            Permesso = 3,
        }
       
    }
}
