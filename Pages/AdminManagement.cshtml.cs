using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using MTLcourts.Data;
using MTLcourts.Models;

namespace MTLcourts.Pages
{
    public class AdminManagementModel : PageModel
    {
        private RoleManager<IdentityRole> roleManager;

        // private CourtsDbContext db;

        // [BindProperty]
        // public InputModel Input { get; set; }
        public AdminManagementModel(RoleManager<IdentityRole> roleManager){
            
            this.roleManager = roleManager;
         
        }
        // public class InputModel
        // {
        
        // [Required]
      
        // public string RoleName { get; set;}
        // }
        
        public List<IdentityRole> LRoles = new List<IdentityRole>();
     
        public async Task OnGetAsync()
        {
          LRoles =roleManager.Roles.ToList();
        }

        
        
        
    public async Task<IActionResult> OnPostAsync(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("AdminManagement");
                
            }
            else
                ModelState.AddModelError("", "No role found");
            return RedirectToAction("AdminManagement");
        }
    }
}
