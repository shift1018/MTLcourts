using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MTLcourts.Data;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContextPool<CourtsDbContext>(options => 
{
        options.UseSqlServer(builder.Configuration.GetConnectionString("CourtsDBConnection"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>options.SignIn.RequireConfirmedAccount= false)
        .AddEntityFrameworkStores<CourtsDbContext>();


//https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-7.0
// sign in using google account, follow the link to add google to register, there is next step to talk about deploying to azure
var configuration = builder.Configuration;
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
    });
builder.Services.Configure<IdentityOptions>(options =>
{
// Password settings. 
options.Password.RequireDigit = true;
options.Password.RequireLowercase = true;
options.Password.RequireNonAlphanumeric = false;
options.Password.RequireUppercase = true;
options.Password.RequiredLength = 6;
options.Password.RequiredUniqueChars = 1;

// Lockout settings.
// Is there a wau to circumvent this for admin users? --> for projects
options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
options.Lockout.MaxFailedAccessAttempts = 5;
options.Lockout.AllowedForNewUsers = true;

// User settings.
options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
options.User.RequireUniqueEmail = true;


// do not require email confirm
options.SignIn.RequireConfirmedAccount = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(25);

    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/AccessDenied";
    options.SlidingExpiration = true;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;

//     var context = services.GetRequiredService<CourtsDbContext>();
//     context.Database.EnsureCreated();
//     // DbInitializer.Initialize(context);
// }

app.UseHttpsRedirection();
app.UseStaticFiles();

// Need to add this
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();


