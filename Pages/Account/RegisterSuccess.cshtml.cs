using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MTLcourts.Pages
{
    public class RegisterSuccessModel : PageModel
    {
          // need to have an email parameter to recieve registered email on page from register class
        [BindProperty(SupportsGet = true)]
        public string UserName { get; set; }
        public void OnGet()
        {
        }
    }
}
