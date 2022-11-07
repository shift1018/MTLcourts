using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MTLcourts.Data;
using MTLcourts.Models;
using Microsoft.EntityFrameworkCore;

namespace MTLcourts.Pages
{
    public class ViewCourtModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly CourtsDbContext db;

        public ViewCourtModel(ILogger<IndexModel> logger, CourtsDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Courts court { get; set; }

        // [BindProperty]
        // public Comments comment { get; set; }

        public List<Comments> courtComments { get; set; }   

        public List<Ratings> courtRating { get; set; } 

        public async Task OnGetAsync()
        {
            court = await db.Court.Include(court => court.User).Where(court => court.Id == Id).FirstOrDefaultAsync();
            courtComments = await db.Comments.Where(comment => comment.CourtsId == Id).ToListAsync();
        }

        // public async Task OnGetAsync()
        // {
        //     court = await db.Court.Include(court => court.User).Where(court => court.Id == Id).FirstOrDefaultAsync();
        //     //courtComments = await db.Comments.Where(comment => comment.CourtsId == Id).ToListAsync();
        //     courtComments = await(from c in db.Comments join u in db.Users on c.User.Id equals u.Id 
        //                     select new
        //                     {
        //                         queryComment = c.Comment,
                                
        //                     });
        // }



        [BindProperty]
        public string Comment { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // if (!ModelState.IsValid)
            // {
            //     return Page();
            // }            
            var userName = User.Identity.Name; 
            var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault(); 
            var courtsId = Id;

            var newComment = new MTLcourts.Models.Comments{
                CourtsId = courtsId,
                User = user,
                DateWhen = DateTime.Now,                
                Comment = Comment,
            };
            db.Comments.Add(newComment);
            await db.SaveChangesAsync();

            return RedirectToPage("ViewCourt", Id);

        }



    }
}
