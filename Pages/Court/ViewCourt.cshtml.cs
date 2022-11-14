
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MTLcourts.Data;
using MTLcourts.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MTLcourts.Pages
{
    public class ViewCourtModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly CourtsDbContext db;

        // private UserManager<IdentityUser> userManager;


        // UserManager<IdentityUser> userManager

        public ViewCourtModel(ILogger<IndexModel> logger, CourtsDbContext db)
        {
            _logger = logger;
            this.db = db;
            // this.userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Courts court { get; set; }

        public Ratings rating { get; set; }

        public Comments comment { get; set; }

        [BindProperty]
        public Checkedin NewCheckIn { get; set; }

        // [BindProperty]
        // public IdentityUser currentuser { get; set; }


        

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
        //  public int PlayersCheckedIn { get; set; }

        [BindProperty]
        public bool IsCheckedIn { get; set; }

        [BindProperty]
         public int NumCheckedIn { get; set; }

        public  void OnGet()
        {
            court =  db.Court.Include(court => court.User).Where(court => court.Id == Id).FirstOrDefault();
            // if (court == null) {
            //             // FIXME
            // }
            courtComments =  db.Comments.Include(comment => comment.User).Where(comment => comment.CourtsId == Id).ToList();




             var userName = User.Identity.Name;
            var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
             var courtsId2 = Id;
           var newcheck =  db.Checkedin.Where(r => r.CourtsId == Id && r.User.UserName == userName).FirstOrDefault();
           
            if ( newcheck != null) {

                NewCheckIn = newcheck;
            }
            else{
            //     var userName2 = User.Identity.Name;
            // var user2 = db.Users.Where(u => u.UserName == userName2).FirstOrDefault();
            //  var courtsId2 = Id;
                
                var newCheckin = new MTLcourts.Models.Checkedin {User = user, CourtsId = courtsId2, Date = DateTime.Now, IsCheckedIn = false, 
              NumCheckedIn = 0 };
              NewCheckIn = newCheckin;
               db.Checkedin.Add(newCheckin);

              db.SaveChanges();
            
              RedirectToPage("ViewCourt", Id);
            }
           

            
            }
            // var userName2 = User.Identity.Name;
            // var user2 = db.Users.Where(u => u.UserName == userName2).FirstOrDefault();
            //  var courtsId2 = Id;

            //     var newCheckin = new MTLcourts.Models.Checkedin {User = user2, CourtsId = courtsId2, Date = DateTime.Now, IsCheckedIn = false, 
            //   NumCheckedIn = 0 };
              
            //    db.Checkedin.Add(newCheckin);

            //   db.SaveChangesAsync();
            
             
            // }

            //  return RedirectToPage("ViewCourt", Id);
            
             
           
        

        public IActionResult OnPostCheckin()
        {
            // Date = @DateTime.Now;
            // DateTime dt = Date.Add(Time.TimeOfDay);
            //  court = db.Court.Where(court => court.Id == Id).FirstOrDefault();

            //-----Comment out-------//
            var userName = User.Identity.Name;
            var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
             var courtsId2 = Id;
             var newcheck =  db.Checkedin.Where(r => r.CourtsId == Id && r.User.UserName == userName).FirstOrDefault();
            //  checkin
            if(newcheck.IsCheckedIn){
                
                newcheck.IsCheckedIn= false;
              
            }
            // check out
            else {
                newcheck.NumCheckedIn= NumCheckedIn;
                newcheck.IsCheckedIn= true;

            }
            db.SaveChangesAsync();
            //-----Comment out-------//
                // court = db.Court.Where(court => court.Id == Id).FirstOrDefault();
                // courtComments = db.Comments.Include(comment => comment.User).Where(comment => comment.CourtsId == Id).ToList();
                // ModelState.AddModelError(string.Empty, "error");
            
            // NewCheckIn = db.Checkedin.Where(r => r.CourtsId == Id).FirstOrDefault();
            // var checkinQuery = db.Checkedin.Where(NewCheckIn => NewCheckIn.CourtsId == Id).ToList();
            // var total = 0;

            // court = db.Court.Where(court => court.Id == Id).FirstOrDefault();
            // foreach (var chek in checkinQuery)
            // {
            // if (NewCheckIn.IsCheckedIn= false)
            // {
            //     court.PlayersCheckedIn = 0;
            // }
            // else
            // {
            //     total += chek.NumCheckedIn;
            // }

            // court.PlayersCheckedIn = total;

            // }
            //-----Comment out-------//
            return RedirectToPage("ViewCourt", Id);
            //-----Comment out-------//
            //   return RedirectToAction("Get");
              
            } 


        //     public IActionResult OnPostCheckout()
        // {

            //    var userName = User.Identity.Name;
            //     var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            //     var courtsId = Id;

            //    var testCheckIn = db.Checkedin.Include(NewCheckIn => NewCheckIn.User).Where(court => court.CourtsId == Id).FirstOrDefault();

            //      testCheckIn.IsCheckedIn = false;

            //       db.SaveChangesAsync();

            
        //     return RedirectToPage("ViewCourt", Id);
        //     //   return RedirectToAction("Get");
              
        //     } 
            
        
        

        public IActionResult OnPostRating()
        {
            if (!ModelState.IsValid)
            {
                court = db.Court.Where(court => court.Id == Id).FirstOrDefault();
                courtComments = db.Comments.Include(comment => comment.User).Where(comment => comment.CourtsId == Id).ToList();
                // ModelState.AddModelError(string.Empty, "error");
//                return RedirectToPage("ViewCourt", Id);
                return Page();
            }
            // else
            // {
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


                rating = db.Ratings.Where(r => r.CourtsId == Id && r.User.UserName == userName).FirstOrDefault();
                var ratingsQuery = db.Ratings.Where(rating => rating.CourtsId == Id).ToList();
                var count = 0;
                var sum = 0;
                court = db.Court.Where(court => court.Id == Id).FirstOrDefault();
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
                    db.SaveChanges();
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
                    db.SaveChanges();

                    return RedirectToPage("ViewCourt", Id);
                }
            // }

            
        }
        
    }
}
