﻿using Models;
using System;
using System.Collections.Generic;
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
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        [Required]
        public string Note { get; set; }
        public RequestTypes RequestType { get; }
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
