using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FudbalskiTurnirWeb.Data;
using FudbalskiTurnirWeb.Interfaces;
using FudbalskiTurnirWeb.Common;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FudbalskiTurnirWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FudbalskiTurnirWebContext") ?? throw new InvalidOperationException("Connection string 'FudbalskiTurnirWebContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<FudbalskiTurnirWebContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IGeneratePairingForMatches, GenerateSingleRoundRobinPairs>();
builder.Services.AddTransient<IMakeLeaderboard, MakeLeaderboard>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
