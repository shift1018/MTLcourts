using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MTLcourts.Data;
using MTLcourts.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        
        public Comments comment { get; set; }

        public List<Comments> courtComments { get; set; }   

        public List<Ratings> courtRating { get; set; } 


        [BindProperty]
        public string Comment { get; set; }

        [BindProperty]
        public int Rating { get; set; }

        public async Task OnGetAsync()
        {
            court = await db.Court.Include(court => court.User).Where(court => court.Id == Id).FirstOrDefaultAsync();
            courtComments = await db.Comments.Where(comment => comment.CourtsId == Id).ToListAsync();

           // calculate average rating 
           
            // var ratingsQuery = db.Ratings.Where(rating => rating.CourtsId == Id).ToListAsync();
            // //courtRatingArr[] =
            // double average = ratingsQuery.Average();
        }

        // public async Task OnGetAsync()
        // {
        //     court = await db.Court.Include(court => court.User).Where(court => court.Id == Id).FirstOrDefaultAsync();
        //     courtComments = await db.Comments.Where(comment => comment.CourtsId == Id).ToListAsync();

        //     var queryCourtComments = await(
        //         from c in db.Comments.Where(comment => comment.CourtsId == Id) 
        //         join u in db.Users 
        //         on c.User.Id equals u.Id 
        //             select new 
        //             {
        //                 queryComment = c.Comment,
                                
        //             });
        // }




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

            var newRating = new MTLcourts.Models.Ratings{
                CourtsId = courtsId,
                User = user,
                Rating = Rating
            };

 



            db.Comments.Add(newComment);
            db.Ratings.Add(newRating);
            await db.SaveChangesAsync();

            return RedirectToPage("ViewCourt", Id);

        }



    }
}
