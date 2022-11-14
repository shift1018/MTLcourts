using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MTLcourts.Data;
using MTLcourts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MTLcourts.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;



    private readonly CourtsDbContext db;

    public IndexModel(ILogger<IndexModel> logger, CourtsDbContext db)
    {
        _logger = logger;
        this.db = db;

    }
    public List<Courts> courtsList { get; set; }

    [BindProperty]
    public decimal CourtLat { get; set; }

    [BindProperty]
    public decimal CourtLong { get; set; }

    public async Task OnGetAsync()
    {
        courtsList = await db.Court.ToListAsync();

    }


    public IActionResult OnPost()
    {
        
        // if (!ModelState.IsValid)
        // {
        //     ModelState.AddModelError(string.Empty, "error");
        //     return Page();
        // }
        // else
        // {
            int nearestCourtId = 0;
            double closestDist = 0;
            var court = db.Court.FirstOrDefault();

            var d1 = (float)CourtLat * (Math.PI / 180.0);
            var num1 = (float)CourtLong * (Math.PI / 180.0);
            var dist2 = (float)court.CourtLatitude * (Math.PI / 180.0);
            var number2 = (float)court.CourtLongitude * (Math.PI / 180.0) - num1;
            var dist3 = Math.Pow(Math.Sin((dist2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(dist2) * Math.Pow(Math.Sin(number2 / 2.0), 2.0);

            closestDist = 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(dist3), Math.Sqrt(1.0 - dist3)));
            Console.WriteLine(closestDist);

            var courtsList = db.Court.ToList();
            foreach (var c in courtsList)
            {

                var d2 = (float)c.CourtLatitude * (Math.PI / 180.0);
                var num2 = (float)c.CourtLongitude * (Math.PI / 180.0) - num1;
                var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
                var distResult = 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));

                if (distResult < closestDist)
                {
                    closestDist = distResult;
                    nearestCourtId = c.Id;
                }
                Console.WriteLine(nearestCourtId);
            }

            // var closestCourt = db.Court.Where(c => c.CourtLatitude == CourtLat && r.User.UserName == userName).FirstOrDefault();
            //return RedirectToPage("Index");
            Console.Write("end");
            return Page();
        // }
    }

// //---------Method to calculate distance to the court------------
//     private static double calcDistance(decimal courtLat, decimal courtLong, decimal lat, decimal lon)
//     {
//         var d1 = (float)courtLat * (Math.PI / 180.0);
//         var num1 = (float)courtLong * (Math.PI / 180.0);
//         var d2 = (float)lat * (Math.PI / 180.0);
//         var num2 = (float)lon * (Math.PI / 180.0) - num1;
//         var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
//         var calcResult = 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
//         return calcResult;
//     }

    //-------------------Ajax call from geocoding shoul get here-----------
    // public IActionResult OnGetGeoData(){
    //     return Content("Hello");

    // }




    // GET: Locations
    // [HttpGet]
    // public ActionResult map()
    // {
    //     var q = (from a in db.Court
    //     select new { a.Name, a.Description, a.Latitude, a.Longitude, a.Name, a.alertType.IconUrl }).OrderBy(a=>a.Name);
    // // return PartialView("_map", q.ToList());
    //     return Json(q, JsonRequestBehavior.AllowGet);
    // }


    // public static void Locations()
    // {
    //     var address = "Stavanger, Norway";

    //     var locationService = new GoogleLocationService();
    //     var point = locationService.GetLatLongFromAddress(address);

    //     var latitude = point.Latitude;
    //     var longitude = point.Longitude;

    //     // Save lat/long values to DB...
    // }


}




