
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using MTLcourts.Data;
using MTLcourts.Models;
using Microsoft.EntityFrameworkCore;


namespace MTLcourts.Pages;
     [Authorize]
public class IndexModel : PageModel
{
    private readonly CourtsDbContext db;
    public IndexModel(CourtsDbContext db) => this.db = db;

 
    public List<Courts> courtsList { get; set; }



    public async Task OnGetAsync()
   {
    courtsList = await db.Court.ToListAsync();
   }

  
}
