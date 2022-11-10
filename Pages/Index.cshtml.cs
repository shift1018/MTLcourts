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


    

    public async Task OnGetAsync()
   {
    courtsList = await db.Court.ToListAsync();

   }

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

   
   

