using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MTLcourts.Models
{
    public class Comments
    {
          public int Id { get; set; }

        public int CourtsId { get; set; }

        public IdentityUser User { get; set; }
        
        //  public int UsersId { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

         public Courts Courts { get; set; }
        //  public Users Users { get; set; }

    }
}