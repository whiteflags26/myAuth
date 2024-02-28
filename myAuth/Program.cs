using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using myAuth.Areas.Identity.Data;
using myAuth.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("myDbContextConnection") ?? throw new InvalidOperationException("Connection string 'myDbContextConnection' not found.");

builder.Services.AddDbContext<myDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<MyAuthUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<myDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.Configure<IdentityOptions>(options=>
{
    options.Password.RequireNonAlphanumeric = false;
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
