using Microsoft.EntityFrameworkCore;
using Role_AuthDemo.Models;

namespace Role_AuthDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<LeaveDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("LeaveManagementDb")));

            //Add these to enable sessions
            builder.Services.AddDistributedMemoryCache(); //saves the session details in the server 
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // user stays logged in for 30 minutes
                options.Cookie.HttpOnly = true; //Protects from attacks and stealing of the session information
                options.Cookie.IsEssential = true;
            });
            /*  ************  */

            var app = builder.Build();

            app.UseSession(); //Configure the application to use sessions

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
