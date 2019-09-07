using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vap.Models
{
    public class CreateAuto
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public AutoTypes AutoType { get; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        [Required]
        public string Note { get; set; }
        public DateTime CollectionDate { get; set; }
        List<Veicle> veicoli = new List<Veicle>();
        public enum AutoTypes
        {
            BMW_classeE = 0,
            FIAT_500L = 1,
            POLO = 2,
            TESLA = 3,
        }
    }
}
