using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace MTLcourts.Models
{
    public class Checkedin
    {
        public int Id { get; set; }

        public IdentityUser User { get; set; }

        [Required]
        public int CourtsId { get; set; }

        [Required]
        public DateTime Date { get; set; }

         public bool IsCheckedIn { get; set; }

        [Range (1, 20)]
        [RegularExpression(@"[1-9]", ErrorMessage = "The number of people should be between 1 and 20")]
        public int NumCheckedIn { get; set; }

        public Courts Courts { get; set; }


    }
}