using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace MTLcourts.Pages
{
    public class RegisterModel : PageModel
    {

         private readonly UserManager<IdentityUser> userManager; 

          private readonly ILogger<RegisterModel> logger;

        //Input is INSTANCE of type inputModel which has as fields email, pass, confpass
       [BindProperty]
       public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

       
       public RegisterModel(UserManager<IdentityUser> userManager, ILogger<RegisterModel> logger) {
            this.userManager = userManager;
            this.logger = logger;
       }

       // inner class is like a grouping of all fields in forms. will recieve it in an instance of type InputModel (done above).
       public class InputModel
       {
        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2 )]
        [Display(Name ="Username" )]
        public string UserName { get; set; } 

        // [Required]
        // [EmailAddress]
        // [Display(Name ="Email" )]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6 )]
        [DataType(DataType.Password)]
        [Display( Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display( Name ="Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
       }

        public void OnGet()
        {
        }


         public async Task<IActionResult> OnPostAsync()
        {
            
            if (ModelState.IsValid)
            {
                // NEED to pick one unique identifier for account
                var user = new IdentityUser {UserName = Input.UserName, Email = Input.Email};
                // {UserName = Input.Email, Email = Input.Email};

                // insert data in Enitity Framework --> create new entity, say "save" or "add" and save changes
                // does 2 things: 1) it will verify the user information (is username unique, if another account exists with same it will fail --> false)
                //2) verifies quality of the password. quality of password configuration will be given in program.cs. Will define configuration globally in Program.cs.
                // those requirements are used by CreateAsync. Using CONFIGURATION instead of CODING.
                // plain text password needs to be encrypted before its put into the intentity user entity (will be done later in configurations on program.cs)
                var result = await userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded) {
                    // If succeeded, logging a message into a log with severity
                    logger.LogInformation($"User {Input.UserName} created a new account with password");
                    var result_role = await userManager.AddToRoleAsync(user, "user");
                    // adding a parameter into the url of registersuccess page (can now say "youve registered with this email" click to login)
                    return RedirectToPage("RegisterSuccess", new {UserName = Input.UserName});
                }
                // If NOT succeeded need to give a list of errors to the template rendering to show back to user
                // errors will show on asp-validation-summary="All" on template page
                foreach (var error in result.Errors) {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If not then render the template again --> redisplayed form will save and show again all previous data
            // system will show the error messages 
             return Page();
        }





    }
}
