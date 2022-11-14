using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MTLcourts.Data;
using MTLcourts.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MTLcourts.Pages
{
    public class IntentionModel : PageModel
    {
    private readonly ILogger<IndexModel> _logger;

    private readonly CourtsDbContext db;
  
    public IntentionModel(ILogger<IndexModel> logger, CourtsDbContext db)
    {
        _logger = logger;
        this.db = db;
    }
    public List<Courts> courtsList { get; set; }

    public List<Intentions> intentionsList { get; set; }

    [BindProperty, DataType(DataType.Date)]
    public DateTime Date { get; set; }
    [BindProperty, DataType(DataType.Time)]
    public DateTime Time { get; set; }
    

    public async Task OnGetAsync()
   {

    courtsList = await db.Court.ToListAsync();
    intentionsList = await db.Intentions.ToListAsync();
// court = await db.Court.Include(court => court.User).Where(court => court.Id == Id).FirstOrDefaultAsync();
   }

    }
}
