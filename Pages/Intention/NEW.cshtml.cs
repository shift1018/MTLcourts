using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MTLcourts.Pages
{
    public class NEWModel : PageModel
    {
        public string Message { get; set; }
        public void OnGet()
        {
        }
         [BindProperty]
public DateTime DateTime { get; set; }
    }
}
