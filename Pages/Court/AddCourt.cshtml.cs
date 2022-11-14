using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using MTLcourts.Data;
using MTLcourts.Models;
using Azure.Storage.Blobs.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Azure;

namespace MTLcourts.Pages

{

    //Only allows logged in users to access this page

     [Authorize(Roles = "Admin")]
    //  [Authorize(Roles = "Admin")]

    public class AddCourtModel : PageModel

    {

      

         private CourtsDbContext db;

         private readonly ILogger<RegisterModel> logger;



         private IWebHostEnvironment _environment;
         private readonly IConfiguration _configuration;

         public AddCourtModel(CourtsDbContext db, ILogger<RegisterModel> logger, 
         IWebHostEnvironment environment, IConfiguration configuration ) {

            this.db = db;

            this.logger = logger;

             _environment = environment;

             _configuration = configuration;

        }

        [BindProperty]
        public IFormFile Upload { get; set; }

        [BindProperty]
        public List<BlobItem> BlobList { get; set; }

        [BindProperty]
        public Courts NewCourt { get; set; }


       
        


        public async Task<IActionResult> OnPostAsync()

        {

            if (ModelState.IsValid)

            {

              string photoUrl = null;

              if (Upload != null) {

              string _postedFileName = Upload.FileName;
              string _fileContentType = Upload.ContentType;
              string _actionMessage = " ";

              try
    {
        string blobstorageconnection = _configuration.GetSection("AzureStorage")["ConnectionString"];
        string containerName = _configuration.GetSection("AzureStorage")["ContainerName"];
            
        // get storage account obect using connection string    
        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
        
        // create the blob client    
        CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
        
        // get container reference.    
        CloudBlobContainer container = blobClient.GetContainerReference(containerName);
        
        // get the blob reference you want to work with    
        CloudBlockBlob blockBlob = container.GetBlockBlobReference(_postedFileName);
       
        //assuming we upload only image from here, else find dynamically 
        blockBlob.Properties.ContentType = _fileContentType; //"image/jpeg";
        
        using (var data = Upload.OpenReadStream())
        {
            await blockBlob.UploadFromStreamAsync(data);
        }

        _actionMessage = "Uploaded Successfully to Blob Storage";
    }
    catch (RequestFailedException ex)
    {
        _actionMessage = ex.ToString();
    }
                    
                photoUrl = Path.Combine("https://mtlcourtsblob.blob.core.windows.net/mtlcourtscontainer/", _postedFileName);
              }       
              var newCourt = new MTLcourts.Models.Courts {PhotoUrl = photoUrl, Name = NewCourt.Name, Address = NewCourt.Address, Description = NewCourt.Description, 
              PostalCode = NewCourt.PostalCode, AvgRating = NewCourt.AvgRating, CourtLatitude = NewCourt.CourtLatitude, CourtLongitude = NewCourt.CourtLongitude };

              db.Court.Add(newCourt);

              await db.SaveChangesAsync();

              return RedirectToPage("AddCourtSuccess");

            }else{
              return Page();
            }

        }

        public void OnGet()

        {

        }

    }

}