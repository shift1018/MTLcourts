using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MTLcourts.Models
{
    public class Ratings
    {
        public int Id { get; set; }

        public int CourtsId { get; set; }

        public IdentityUser User { get; set; }

        // [Required]
        // [Range (1, 5)]
        public int Rating { get; set; }

         public Courts Courts { get; set; }
    }
}