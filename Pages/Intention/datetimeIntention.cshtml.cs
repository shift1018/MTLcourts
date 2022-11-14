using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MTLcourts.Data;
using MTLcourts.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MTLcourts.Pages
{
    public class datetimeIntentionModel : PageModel
    {
    private readonly ILogger<IndexModel> _logger;

    private readonly CourtsDbContext db;
    private UserManager<IdentityUser> userManager;
  
    public datetimeIntentionModel(UserManager<IdentityUser> userManager, ILogger<IndexModel> logger, CourtsDbContext db)
    {
        this.userManager = userManager; 
        _logger = logger;
        this.db = db;
    }
    public IdentityUser currentuser { get; set; }

    public List<Intentions> intentionsList { get; set; }

    public Intentions intention  { get; set; }

    [BindProperty(SupportsGet =true)]
    public int NumOfPeople { get; set; } 

    public int sum { get; set; }  =0 ;

    public async Task OnGetAsync(int id, DateTime date)
    {

        intentionsList= await db.Intentions.Include(intention => intention.User).Where(i=>i.CourtsId==id && i.Date ==date).ToListAsync();             
        foreach (var item in intentionsList)
        {
            sum += item.NumOfPeople;
        }
        
    }

    public async Task<IActionResult> OnPostAsync(int id, DateTime date)
            {
            
                DateTime dt = date;
                var courtsId = id;
                var username = User.Identity.Name;
                int np = NumOfPeople;

//if the sign in user is in the list of intention, if yes we modify the numofpeople, or not we will add a new intention with the same courtsid, datetime
                var intention =  db.Intentions.Where(r =>r.User.UserName == username ).FirstOrDefault();
                if(intention!=null ){ //yes
                    intention.NumOfPeople = np;
                }
                else {// empty
                     intention = new MTLcourts.Models.Intentions {
                    CourtsId = courtsId,
                    User = await userManager.FindByNameAsync(username),
                    Date = dt, 
                    NumOfPeople = np
                    };
                    db.Intentions.Add(intention);
                 }

                await db.SaveChangesAsync();
                
                return RedirectToPage("SuccessIntention");
            }



    }
}
