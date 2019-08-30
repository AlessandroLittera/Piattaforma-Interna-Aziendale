using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vap.Models
{
    public  class CreateUser
    {

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }
        [Required]
        public string Nickname { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@".*@vetrya\.com$", ErrorMessage = "Invalid domain in email address. The domain must be vetrya.com")]
        public string Email { get; set; }

        [Required]
        public string Image { get; set; }
        public bool IsMailingList { get; set; }

        public AccountantTypes AccountType { get; set; }
    }
}
