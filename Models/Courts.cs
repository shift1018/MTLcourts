using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTLcourts.Models
{
    public class Courts
    {
         public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string Description { get; set; }

        public string PhotoUrl { get; set; }

        public double AvgRating { get; set; }

        
        
    }
}