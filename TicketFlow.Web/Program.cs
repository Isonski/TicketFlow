<<<<<<< HEAD
using TicketFlow.Services.Interfaces;
using TicketFlow.Services.Implementations;
using TicketFlow.Data;
using Microsoft.EntityFrameworkCore;
namespace TicketFlow.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<TicketFlowDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IEventService, EventService>();


            var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Events}/{action=Index}/{id?}");

            app.Run();
        }
    }
=======
using Microsoft.EntityFrameworkCore;
using TicketFlow.Services.Interfaces;
using TicketFlow.Services.Implementations;
using TicketFlow.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<TicketFlowDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
>>>>>>> eecc1bbf71c5df66ab798a52c353a2446c9b5ada
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
