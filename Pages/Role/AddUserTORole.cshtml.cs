using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

namespace MTLcourts.Pages
{
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
        public List<IdentityUser> LUsers = new List<IdentityUser>();
        IdentityRole Role;

        public class UserRoleModel{
            public string UserId { get; set; }

            public string UserName { get; set; }
            public bool IsSelected { get; set; }
        }

        public async Task OnGetAsync(string Id)
        {
    
           LUsers = userManager.Users.ToList() ;
        }
  
        public async Task<IActionResult> OnPostAsync(string id)
        {
            Role =await roleManager.FindByIdAsync(Id);
            var UserRole = new List<UserRoleModel>();
            
            foreach (var user in LUsers)
            {
              
                // var role = await userManager.GetRolesAsync(user);
                // if(role==null){
                //     var result_role = await userManager.AddToRoleAsync(user, Role.Name);
                // }
                var userRoleModel = new UserRoleModel{
                    UserId = user.Id,
                    UserName= user.UserName,
                    
                };
                var result = await userManager.IsInRoleAsync(user, Role.Name);
                
                if(result){
                    userRoleModel.IsSelected = true;
                }else{userRoleModel.IsSelected = false;}
                UserRole.Add(userRoleModel);
            }
            
            for (int i = 0; i<UserRole.Count; i++){
                var user = await userManager.FindByEmailAsync(UserRole[i].UserId);
                IdentityResult result = null;
                if(UserRole[i].IsSelected && !(await userManager.IsInRoleAsync(user,Role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, Role.Name);
                }
                else if(!UserRole[i].IsSelected && !(await userManager.IsInRoleAsync(user,Role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, Role.Name);
                }
                else
                {
                    continue;
                }
            }
            
            return RedirectToAction("AdminManagement");
        }

    
    }
}
