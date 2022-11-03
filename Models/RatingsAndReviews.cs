using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MTLcourts.Models
{

    
    public class RatingsAndReviews
    {
        public int Id { get; set; }

        public int CourtsId { get; set; }

         public IdentityUser User { get; set; }

        public string Name { get; set; }

        public DateTime WhenDateTime { get; set; }

        public int Rating { get; set; }

        public string Review { get; set; }

         public Courts Courts { get; set; }

       
    }
}