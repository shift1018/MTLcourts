using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using MTLcourts.Data;
using MTLcourts.Models;

namespace MTLcourts.Pages
{
    public class CreateRoleModel : PageModel
    {
        
    
        private RoleManager<IdentityRole> roleManager;

        

        [BindProperty]
        public InputModel Input { get; set; }
        public CreateRoleModel(RoleManager<IdentityRole> roleManager){
            
            this.roleManager = roleManager;
         
        }
        public class InputModel
        {
        
        [Required]
      
        public string RoleName { get; set;}
        }
        
        // public List<IdentityRole> LRoles = new List<IdentityRole>();
     
        // public async Task OnGetAsync()
        // {
        //   LRoles =roleManager.Roles.ToList();
        // }

        
        public async Task<IActionResult> OnPostAsync()
        {
        
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                    role.Name = Input.RoleName;
                    IdentityResult result =roleManager.CreateAsync(role).Result;

                if(result.Succeeded){
                    return RedirectToPage("AdminManagement");
                }
                // foreach (IdentityError error in result.Errors)
                // {
                //     ModelState.AddModelError("",error.Description);
                // }
            }

        return Page();
        }
        
    }
    }

