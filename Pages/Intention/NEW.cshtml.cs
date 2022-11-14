using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MTLcourts.Data;
using MTLcourts.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MTLcourts.Pages
{

    public class NEWModel : PageModel
    {
    private UserManager<IdentityUser> userManager;

    private readonly ILogger<IndexModel> _logger;

    private readonly CourtsDbContext db;
  
    public NEWModel(UserManager<IdentityUser> userManager, ILogger<IndexModel> logger, CourtsDbContext db)
    {
        this.userManager = userManager;
        _logger = logger;
        this.db = db;
    }
    [BindProperty, DataType(DataType.Date)]
    public DateTime Date { get; set; }
    [BindProperty, DataType(DataType.Time)]
    public DateTime Time { get; set; }
    [BindProperty]
    public IdentityUser currentuser { get; set; }

    [BindProperty]
    public int CourtsId  { get; set; }

    [BindProperty]
    public int NumOfPeople  { get; set; }

    public List<Courts> courtsList { get; set; }
    public async Task OnGetAsync()
    {   Date = DateTime.Now.Date;
        courtsList = await db.Court.ToListAsync();
    }
    public async Task<IActionResult> OnPostAsync()
            {
                // Date =Date;
                DateTime dt = Date.Add(Time.TimeOfDay);
                var courtsId = int.Parse(Request.Form["CourtsId"]);
                var userName = User.Identity.Name;
                int np = NumOfPeople;
                var newintention =  db.Intentions.Where(r => r.Date == dt  && r.User.UserName == userName ).FirstOrDefault();
                if(newintention!=null ){
                    newintention.NumOfPeople = np;
                    newintention.CourtsId =courtsId;
                }
                else {
                     var intention = new MTLcourts.Models.Intentions {
                    CourtsId = courtsId,
                    User = await userManager.FindByIdAsync(currentuser.Id),
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


