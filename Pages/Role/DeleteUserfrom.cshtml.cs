
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;



namespace MTLcourts.Pages
{
    public class DeleteUserfromModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public string Id { get; set; } 
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string RoleName { get; set; }
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public DeleteUserfromModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager){
                    this.userManager = userManager; 
                    this.roleManager = roleManager; 
                }

        public IdentityUser User;
        public async Task OnGetAsync(string Id)
            {
                User =await userManager.FindByIdAsync(Id);

                var  roles = await userManager.GetRolesAsync(User);
                 
                RoleName = roles[0];
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
              
                var resultremove = await userManager.RemoveFromRoleAsync(User, RoleName);
           

        

                return RedirectToPage("Success");
            }
    }
}





