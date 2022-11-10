using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MTLcourts.Models;
using MTLcourts.Data;
using Microsoft.EntityFrameworkCore;

namespace MTLcourts.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly CourtsDbContext db;
        public DeleteModel(CourtsDbContext db) => this.db = db;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public Courts Court { get; set; }


        public void OnGet()
        {
            Court = db.Court.Find(Id);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var CourtFromDB = await db.Court.FindAsync(Id);
            db.Court.Remove(CourtFromDB);
            await db.SaveChangesAsync();

            return RedirectToPage("DeleteSuccess");
        }
    }
}
