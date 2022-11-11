
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MTLcourts.Data;
using MTLcourts.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.ComponentModel.DataAnnotations;

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

        public Ratings rating { get; set; }

        public Comments comment { get; set; }

        public Checkedin NewCheckIn { get; set; }


        

        public List<Comments> courtComments { get; set; }

        public List<Ratings> courtRating { get; set; }


        [BindProperty]
        [Required(ErrorMessage = "Review can not be empty")]
        public string Comment { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Set your rating to submit")]
        public int Rating { get; set; }

        [BindProperty]
        public double AvgRating { get; set; }

        // [BindProperty]
        // public bool IsCheckedIn { get; set; }

        // [BindProperty]
        //  public int NumCheckedIn { get; set; }

        public async Task OnGetAsync()
        {
            court = await db.Court.Include(court => court.User).Where(court => court.Id == Id).FirstOrDefaultAsync();
            if (court == null) {
                        // FIXME
            }
            courtComments = await db.Comments.Include(comment => comment.User).Where(comment => comment.CourtsId == Id).ToListAsync();

        }

        public async Task<IActionResult> OnPostAsyncCheckin()
        {
            // Date = @DateTime.Now;
            // DateTime dt = Date.Add(Time.TimeOfDay);
            var userName = User.Identity.Name;
            var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
             var courtsId = Id;

             if (!ModelState.IsValid)
            {
                court = await db.Court.Where(court => court.Id == Id).FirstOrDefaultAsync();
                courtComments = await db.Comments.Include(comment => comment.User).Where(comment => comment.CourtsId == Id).ToListAsync();
                ModelState.AddModelError(string.Empty, "error");
                return Page();
            }
            else
            {
               
                var newCheckin = new MTLcourts.Models.Checkedin {User = user, CourtsId = courtsId, Date = DateTime.Now, IsCheckedIn = NewCheckIn.IsCheckedIn, 
              NumCheckedIn = NewCheckIn.NumCheckedIn };
              
               db.Checkedin.Add(newCheckin);

              await db.SaveChangesAsync();
            
            return RedirectToPage("ViewCourt", Id);
            //   return RedirectToAction("Get");
              
            } 
            
        }
        

        public async Task<IActionResult> OnPostAsyncRating()
        {
            if (!ModelState.IsValid)
            {
                court = await db.Court.Where(court => court.Id == Id).FirstOrDefaultAsync();
                courtComments = await db.Comments.Include(comment => comment.User).Where(comment => comment.CourtsId == Id).ToListAsync();
                ModelState.AddModelError(string.Empty, "error");
//                return RedirectToPage("ViewCourt", Id);
                return Page();
            }
            else
            {
                var userName = User.Identity.Name;
                var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
                var courtsId = Id;

                var newComment = new MTLcourts.Models.Comments
                {
                    CourtsId = courtsId,
                    User = user,
                    DateWhen = DateTime.Now,
                    Comment = Comment,
                };

                var newRating = new MTLcourts.Models.Ratings
                {
                    CourtsId = courtsId,
                    User = user,
                    Rating = Rating
                };


                rating = await db.Ratings.Where(r => r.CourtsId == Id && r.User.UserName == userName).FirstOrDefaultAsync();
                var ratingsQuery = await db.Ratings.Where(rating => rating.CourtsId == Id).ToListAsync();
                var count = 0;
                var sum = 0;
                court = await db.Court.Where(court => court.Id == Id).FirstOrDefaultAsync();
                if (rating != null)
                {
                    //ModelState.AddModelError(string.Empty, "You already rated this court. Review only is submitted.");
                    rating.Rating = Rating;
                    foreach (var rat in ratingsQuery)
                    {
                        sum += rat.Rating;
                        count++;
                    }
                    double AvgRatingNewUpdated = Math.Round((double)sum / (double)count, 2);
                    court.AvgRating = AvgRatingNewUpdated;

                    db.Comments.Add(newComment);
                    await db.SaveChangesAsync();
                    return RedirectToPage("ViewCourt", Id);
                }
                else
                {
                    foreach (var rat in ratingsQuery)
                    {
                        sum += rat.Rating;
                        count++;
                    }
                    double AvgRatingNew = Math.Round((double)(sum + Rating) / (double)(count + 1), 2);
                    court.AvgRating = AvgRatingNew;

                    db.Comments.Add(newComment);
                    db.Ratings.Add(newRating);
                    await db.SaveChangesAsync();

                    return RedirectToPage("ViewCourt", Id);
                }
            }

            
        }
        
    }
}
