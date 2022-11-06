using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MTLcourts.Models
{
    public class Courts
    {
         public int Id { get; set; }

        [Required, MinLength(1), MaxLength(50) ]
        public string Name { get; set; }

        [Required, MinLength(1), MaxLength(100) ]      
        public string Address { get; set; }

        [Required, MinLength(4), MaxLength(10) ]
        [RegularExpression(@"^[A-Z0-9-]*$", ErrorMessage = "The postal code should be 4-10 characters and include numbers, uppercase and '-'")]  
        public string PostalCode { get; set; }



       
        public string Description { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        
        public double AvgRating { get; set; }

        public ICollection<Comments>? Comments { get; set; }
         public ICollection<Ratings>? Ratings { get; set; }
         public ICollection<Intentions>? Intentions { get; set; }

         public ICollection<IdentityUser>? User { get; set; }
        public ICollection<Checkedin>? Checkedin { get; set; }
    }
}