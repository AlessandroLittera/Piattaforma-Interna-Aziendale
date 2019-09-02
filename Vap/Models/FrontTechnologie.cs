﻿using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vap.Models
{
    public class FrontTechnologie
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string InCharge { get; set; }

        public ICollection<Role> Roles { get; set; }

        public FrontTechnologie()
        {
            this.Id = string.Empty;
            this.Name = string.Empty;
            this.InCharge = string.Empty;
            this.Roles = new HashSet<Role>();
        }
    }
}
