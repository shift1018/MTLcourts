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
    // public List<Intentions> intList { get; set; }
    [BindProperty, DataType(DataType.Date)]
    public DateTime Date { get; set; }
    [BindProperty, DataType(DataType.Time)]
    public DateTime Time { get; set; }

   
    

    public async Task OnGetAsync()
   {
    Date = DateTime.Now.Date;
    // var Datestr = Date.ToString("yyyy-MM-dd");  
    
    courtsList = await db.Court.ToListAsync();
    // intentionsList = await db.Intentions.Where(i =>i.Date.Date == Date).ToListAsync();
    // var intList = intentionsList.Distinct( );
    //  var intList = intentionsList().Select(book => new { book.Author, book.Title })
    //                                    .Distinct();

    var Intentionquery = from intention in db.Intentions
                            where intention.Date.Date == Date
                            select  new { CourtsId= intention.CourtsId, Date = intention.Date};
    var intList = Intentionquery.Distinct().ToList();

   }

    }
}
// var custQuery =
//     from cust in customers
//     group cust by cust.City into custGroup
//     where custGroup.Count() > 2
//     orderby custGroup.Key
//     select custGroup;