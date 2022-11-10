using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MTLcourts.Pages
{
    [Authorize(Roles = "Admin, Moderator")]
    public class AddUserToModel : PageModel
    {
        
        

        [BindProperty(SupportsGet =true)]
        public string Id { get; set; } 
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string RoleName { get; set; }
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public AddUserToModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager){
                    this.userManager = userManager; 
                    this.roleManager = roleManager; 
                }

        public IdentityUser User;
        public async Task OnGetAsync(string Id)
            {
                User =await userManager.FindByIdAsync(Id);
                Name = User.UserName;
            }


        public async Task<IActionResult> OnPostAsync(string id)
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                
                // role.Name = Name;

                User =await userManager.FindByIdAsync(Id);
                // var  roles = await userManager.GetRolesAsync(User);
                // var roleold = roles[0];
                // var resultremove = await userManager.RemoveFromRoleAsync(User, roleold);
                var resultadd = await userManager.AddToRoleAsync(User, RoleName);

        

                return RedirectToPage("Success");
            }
    }
}


// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.AspNetCore.Identity;
// using System.ComponentModel.DataAnnotations;
// using MTLcourts.Data;
// using MTLcourts.Models;

// namespace MTLcourts.Pages
// {
//     public class AddUserToModel : PageModel
//     {
//         [BindProperty(SupportsGet =true)]
//         public string Id { get; set; }
//         [BindProperty]
//         public string Name { get; set; }

//         private RoleManager<IdentityRole> roleManager;

//          public AddUserToModel(RoleManager<IdentityRole> roleManager){
//             this.roleManager = roleManager;
//         }

//         public IdentityRole role;
//         public async Task OnGetAsync(string Id)
//         {
//            role =await roleManager.FindByIdAsync(Id);
//            Name = role.Name;
//         }
//         // public void OnGet(string Id)
//         // {
//         //    role =roleManager.GetRoleIdAsync(Id);
//         //    Name = role.Name;
//         // }
        
        
//     public async Task<IActionResult> OnPostAsync(string id)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return Page();
//             }
//             IdentityRole role = await roleManager.FindByIdAsync(id);
//             role.Name = Name;
//             var result = await roleManager.UpdateAsync(role);


//                 if(result.Succeeded){
//                     return RedirectToPage("AdminManagement");
//                 }

//             return Page();
//         }

    
//     }
// }
