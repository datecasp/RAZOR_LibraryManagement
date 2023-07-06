using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RAZOR_LibraryManagement.Infra.DataContext;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddDbContext<LM_DbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("connStr"));
});


//For use Identity
builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("authStr"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.Configure<AppSettingsEntity>(builder.Configuration.GetSection("AppConfigs"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//If not logged, redirects to Index, where login lives
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Index";
});

builder.Services.ResolveDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
