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

        
        public Comments comment { get; set; }

        public List<Comments> courtComments { get; set; }   

        public List<Ratings> courtRating { get; set; } 


        [BindProperty]
        public string Comment { get; set; }

        [BindProperty]
        public int Rating { get; set; }

        [BindProperty]
        public double AvgRating { get; set; }

        public async Task OnGetAsync()
        {
            court = await db.Court.Include(court => court.User).Where(court => court.Id == Id).FirstOrDefaultAsync();
            courtComments = await db.Comments.Include(comment => comment.User).Where(comment => comment.CourtsId == Id).ToListAsync();

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

           //calculate average rating 
// !!!!!!!!!!! need to fix the query - giving an error
            // var findUser = await db.Ratings.Where(r => r.CourtsId == Id && r.User.UserName == userName).ToListAsync();
            // if (findUser != null){
            //     ModelState.AddModelError(string.Empty, "You already rated this court in the past.");
            //     return Page();
            // }else{
            // var ratingsQuery = await db.Ratings.Where(rating => rating.CourtsId == Id).ToListAsync();
            // var count = 0;
            // var sum = 0;
            // foreach (var rat in ratingsQuery)
            // {
            //     sum += rat.Rating;
            //     count++;
            // }

            // double AvgRatingNew = Math.Round((double)(sum+Rating)/(double)(count+1), 2); 
            // court = await db.Court.Where(court => court.Id == Id).FirstOrDefaultAsync();
            // court.AvgRating = AvgRatingNew;


            // db.Comments.Add(newComment);
            // db.Ratings.Add(newRating);
            // //db.Court.Update(court.AvgRating = AvgRating1).Where(court => court.Id == Id);
            // await db.SaveChangesAsync();

            // return RedirectToPage("ViewCourt", Id);                
            // }

            var findUser = await db.Ratings.Where(r => r.CourtsId == Id && r.User.UserName == userName).ToListAsync();
            if (findUser != null){
                ModelState.AddModelError(string.Empty, "You already rated this court in the past.");
                return Page();
            }else{
            var ratingsQuery = await db.Ratings.Where(rating => rating.CourtsId == Id).ToListAsync();
            var count = 0;
            var sum = 0;
            foreach (var rat in ratingsQuery)
            {
                sum += rat.Rating;
                count++;
            }

            double AvgRatingNew = Math.Round((double)(sum+Rating)/(double)(count+1), 2); 
            court = await db.Court.Where(court => court.Id == Id).FirstOrDefaultAsync();
            court.AvgRating = AvgRatingNew;


            db.Comments.Add(newComment);
            db.Ratings.Add(newRating);
            //db.Court.Update(court.AvgRating = AvgRating1).Where(court => court.Id == Id);
            await db.SaveChangesAsync();

            return RedirectToPage("ViewCourt", Id);                
            }


        }



    }
}