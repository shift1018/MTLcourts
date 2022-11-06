using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.AspNetCore.Authorization;
using MTLcourts.Data;
using MTLcourts.Models;

namespace MTLcourts.Pages

{
    //Only allows logged in users to access this page
     [Authorize]
    public class AddCourtModel : PageModel
    {
         private CourtsDbContext db;
         private readonly ILogger<RegisterModel> logger;

         public AddCourtModel(CourtsDbContext db, ILogger<RegisterModel> logger) {
            this.db = db;
            this.logger = logger;
        } 

         [BindProperty]
        public Courts NewCourt { get; set; }


         public async Task<IActionResult> OnPostAsync()
        {
            
             if (!ModelState.IsValid)
            {
                return Page();
                 }
               //schedule for insert
            db.Court.Add(NewCourt);

            // db.SaveChanges() --> without Async;
            //perform the insert
            await db.SaveChangesAsync();
              

                return RedirectToPage("AddCourtSuccess");
    }





        public void OnGet()
        {
        }
    }


    
}
