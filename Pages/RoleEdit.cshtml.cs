using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using MTLcourts.Data;
using MTLcourts.Models;

namespace MTLcourts.Pages
{
    public class RoleEditModel : PageModel
    {
        
        public string Id { get; set; }

        private RoleManager<IdentityRole> roleManager;

        public IdentityRole role;
        public async Task OnGetAsync(string Id)
        {
        
    //       LUsers = userManager.Users.ToList();
           role =await roleManager.FindByIdAsync(Id);
        }

        
    }
}
