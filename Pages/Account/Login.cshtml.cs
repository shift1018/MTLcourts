using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace MTLcourts.Pages
{
    public class LoginModel : PageModel
    {

         private readonly SignInManager<IdentityUser> signInManager;
          private readonly ILogger<RegisterModel> logger;
        
         //Input is INSTANCE of type inputModel which has as fields email, pass, confpass
       [BindProperty]
       public InputModel Input { get; set; }

        public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<RegisterModel> logger) {
           
            this.signInManager = signInManager;
            this.logger = logger;
       }

        public class InputModel
       {
        [Required]
        [EmailAddress]
        [Display(Name ="Email" )]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

       }

     //Handle the submission
        public async Task<IActionResult> OnPostAsync()
        {
            // did the validation pass?
            if (ModelState.IsValid)
            {
                // last two params (false, true) refer to remember me (false) and lockout after multiple failure attempts (true)
                var result = await signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, true);
                if (result.Succeeded) {
                    // If succeeded, logging a message into a log with severity
                    logger.LogInformation($"User {Input.Email} logged in");
                    // adding a parameter into the url of registersuccess page (can now say "youve registered with this email" click to login)
                    return RedirectToPage("LoginSuccess");
                } else { // user does not exist, password ivlaid, account locked out --> dont tell user why
                      ModelState.AddModelError(string.Empty, "Login failed");
                }
               
            }
            // If not then render the template again --> redisplayed form will save and show again all previous data
            // system will show the error messages 
             return Page();
        }




        public void OnGet()
        {
        }
    }
}
