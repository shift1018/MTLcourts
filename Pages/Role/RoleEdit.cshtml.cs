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
        [BindProperty(SupportsGet =true)]
        public string Id { get; set; }
        [BindProperty]
        public string Name { get; set; }

        private RoleManager<IdentityRole> roleManager;

         public RoleEditModel(RoleManager<IdentityRole> roleManager){
            this.roleManager = roleManager;
        }

        public IdentityRole role;
        public async Task OnGetAsync(string Id)
        {
           role =await roleManager.FindByIdAsync(Id);
           Name = role.Name;
        }
        // public void OnGet(string Id)
        // {
        //    role =roleManager.GetRoleIdAsync(Id);
        //    Name = role.Name;
        // }
        
        
    public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            IdentityRole role = await roleManager.FindByIdAsync(id);
            role.Name = Name;
            var result = await roleManager.UpdateAsync(role);


                if(result.Succeeded){
                    return RedirectToPage("AdminManagement");
                }

            return Page();
        }

    
    }
}
