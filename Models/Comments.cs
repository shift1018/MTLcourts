using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MTLcourts.Models
{
    public class Comments
    {
          public int Id { get; set; }

        [Required]
        public int CourtsId { get; set; }

        [Required]
        public IdentityUser User { get; set; }
        
        //  public int UsersId { get; set; }
        [Required]
        public DateTime DateWhen { get; set; }

        [Required, MinLength(2), MaxLength(500)]
        public string Comment { get; set; }

         public Courts Courts { get; set; }
        //  public Users Users { get; set; }

    }
}