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
    // [BindProperty]
    // public List<Intentions> intentionsList { get; set; }
    
    [BindProperty, DataType(DataType.Date)]
    public DateTime Date { get; set; } 
    [BindProperty, DataType(DataType.Time)]
    public DateTime Time { get; set; }
    
   
    public class newintention{
            public int CourtsId { get; set; }
            public DateTime Date { get; set; }
    }
    public List<newintention> intList { get; set; }

    public async Task OnGetAsync()
   {
    // Date = ;
    courtsList = await db.Court.ToListAsync();

    var Intentionquery = from intention in db.Intentions
                            where intention.Date.Date == Date
                            select  new newintention { CourtsId= intention.CourtsId, Date = intention.Date};
     intList = Intentionquery.Distinct().ToList();
   }

    public async Task<IActionResult> OnPostAsync()
            {
                
                Date = Date;

               return Page();
            }

}
}
