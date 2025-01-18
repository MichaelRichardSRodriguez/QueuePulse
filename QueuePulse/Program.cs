
using Microsoft.EntityFrameworkCore;
using QueuePulse.DataAccess.Data;
using QueuePulse.DataAccess.Services;
using QueuePulse.DataAccess.UnitOfWork;
using QueuePulse.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using QueuePulse.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add Dbcontext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("QueuePulseConnection")));
builder.Services.AddRazorPages(); //Service To Allow the use of Razor Pages then Add the Mapping of Razor pages below

//This logs-in you automatically once successfully registered. IdentityRole is Added
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); //.AddDefaultIdentity<IdentityUser>" is the default value
//<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true) //This Requires Confirmation and You Need to Login Manually

//Overwrite Default Identity Configuration
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IDepartmentManagementService,DepartmentManagementService>();
builder.Services.AddScoped<IServiceManagementService, ServiceManagementService>();
builder.Services.AddScoped<IQueueGroupService, QueueGroupService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

//Handle Circular Reference Globally
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); //To Check the Role of The User, Authentication First Before Authorization
app.UseAuthorization();
app.MapRazorPages(); //Map the Razor Pages
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Guest}/{controller=Home}/{action=Index}/{id?}");

app.Run();


