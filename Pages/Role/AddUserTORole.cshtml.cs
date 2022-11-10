using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MTLcourts.Pages
{
    [Authorize(Roles = "Admin, Moderator")]
    public class AddUserTORoleModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public string Id { get; set; } 
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;

         public AddUserTORoleModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager){
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        
        public IdentityRole Role;

        public class UserRoleModel{
            [BindProperty]
            public string UserId { get; set; }
            [BindProperty]
            public string UserName { get; set; }
            [BindProperty]
            public string UserEmail { get; set; }
            [BindProperty]
            public bool IsSelected { get; set; } 
        }

    
        public List<UserRoleModel> UserRoleSelected = new List<UserRoleModel>();
        public List<UserRoleModel> userRole= new List<UserRoleModel>();
        // public List<UserRoleModel> LUsers = new List<UserRoleModel>();
        public async Task OnGetAsync(string Id)
        {
            Role =await roleManager.FindByIdAsync(Id);
            var LUsers = userManager.Users.ToList() ;

                var user1 =await userManager.FindByIdAsync("cdb87882-08dd-4829-b964-4c014e7621b0");
var result = await userManager.AddToRoleAsync(user1, "Moderator");
            // foreach(var user in LUsers){


            //     var userRoleModel = new UserRoleModel{
            //         UserId = user.Id,
            //         UserName= user.UserName,
            //         UserEmail= user.Email
            //     };
            //     if(await userManager.IsInRoleAsync(user, Role.Name)){
            //         userRoleModel.IsSelected = true;
            //     }   else{
            //     userRoleModel.IsSelected = false;
            //     }
            //     userRole.Add(userRoleModel);
            // }
           
        }
  
        public async Task<IActionResult> OnPostAsync(List<UserRoleModel> userRole)
        {
           
            // foreach(var user in userRole){
            for (int i = 0; i<userRole.Count; i++){
            var user1 = await userManager.FindByEmailAsync(userRole[i].UserId);
            userRole[i].IsSelected = userRole[i].IsSelected;
            IdentityResult result = null;

            if(userRole[i].IsSelected && !(await userManager.IsInRoleAsync(user1,Role.Name)))
            {
                result = await userManager.AddToRoleAsync(user1, Role.Name);
            }
            else if(!userRole[i].IsSelected && (await userManager.IsInRoleAsync(user1,Role.Name)))
            {
                result = await userManager.RemoveFromRoleAsync(user1, Role.Name);
            }
            else
            {
                continue;
            }
            }
             return RedirectToAction("./AdminManagement");
            
        }
    }

}












       //     var user = await userManager.FindByIdAsync(UserID);
        //     var roles = await userManager.GetRolesAsync(user);

        //     if (user == null)
        //     {
        //         return Page();
        //     }
        
        //     result = await userManager.RemoveFromRolesAsync(user, roles);
        //     if (!result.Succeeded)
        //     {
        //         ModelState.AddModelError("", "Cannot remove user existing roles");
        //         return Page();;
        //     }
        //     result = await userManager.AddToRolesAsync(user, userRole.Where(x => x.IsSelected).Select(y => y.UserName));
        //     if (!result.Succeeded)
        //     {
        //         ModelState.AddModelError("", "Cannot add selected roles to user");
        //         return RedirectToAction("./AdminManagement");
        //     }
        //     // return RedirectToAction("Index");
        // return RedirectToAction("./AdminManagement");