using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MTLcourts.Data;
using MTLcourts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MTLcourts.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

  

     private readonly CourtsDbContext db;

    public IndexModel(ILogger<IndexModel> logger, CourtsDbContext db)
    {
        _logger = logger;
        this.db = db;
    
    }
    public List<Courts> courtsList { get; set; }

    public async Task OnGetAsync()
   {
    courtsList = await db.Court.ToListAsync();

   }

   
   
}
