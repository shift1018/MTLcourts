using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MTLcourts.Models
{
    public class Intentions
    {
         public int Id { get; set; }

        [Required]
        public int CourtsId { get; set; }

        [Required]
        public IdentityUser User { get; set; }
        
        //  public int UsersId { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Range (1, 20)]
        [RegularExpression(@"[1-9]", ErrorMessage = "The number of people should be between 1 and 20")]
        public int NumOfPeople { get; set; }

         public Courts Courts { get; set; }
        //  public Users Users { get; set; }


        
    }
}