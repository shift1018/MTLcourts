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

         private IWebHostEnvironment _environment;

         public AddCourtModel(CourtsDbContext db, ILogger<RegisterModel> logger, IWebHostEnvironment environment) {
            this.db = db;
            this.logger = logger;
             _environment = environment;
        } 

         [BindProperty]
        public Courts NewCourt { get; set; }

        [BindProperty]
        public IFormFile Upload { get; set; }


         public async Task<IActionResult> OnPostAsync()
        {
            
             if (ModelState.IsValid)
            {
                

             string photoUrl = null;
            if (Upload != null) {
                string fileExtension = Path.GetExtension(Upload.FileName).ToLower();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".gif", ".png" };
                 if (!allowedExtensions.Contains(fileExtension)) {
            // Display error and the form again
            ModelState.AddModelError(string.Empty, "Only image files (jpg, jpeg, gif, png) are allowed");
            return Page();
          }

        var invalids = System.IO.Path.GetInvalidFileNameChars();
          var newFileName = String.Join("_", Upload.FileName.Split(invalids, StringSplitOptions.RemoveEmptyEntries) ).TrimEnd('.');
          var destPath = Path.Combine(_environment.ContentRootPath, "wwwroot", "Uploads", Upload.FileName);

           try {
            using (var fileStream = new FileStream(destPath, FileMode.Create))
            {
                Upload.CopyTo(fileStream);
            }
          } catch (Exception ex) when (ex is IOException || ex is SystemException) {
            // TODO: Log this as an error
            ModelState.AddModelError(string.Empty, "Internal error saving the uploaded file");
            return Page();
          }
           photoUrl = Path.Combine("Uploads", newFileName);

            }
            var newCourt = new MTLcourts.Models.Courts {PhotoUrl = photoUrl, Name = NewCourt.Name, Address = NewCourt.Address, Description = NewCourt.Description, PostalCode = NewCourt.PostalCode, AvgRating = NewCourt.AvgRating };
            db.Court.Add(newCourt);

            await db.SaveChangesAsync();
    
            return RedirectToPage("NewCourtSuccess");
            }
    return Page();
            }

            public void OnGet()
        {
        }
    }


    
}
