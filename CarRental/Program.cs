using CarRental.Data;
using CarRental.Repositories.Implementations;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Implementations;
using CarRental.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register ApplicationDbContext and configure it to use SQL Server 
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("CarRentalDbConnection")));

            // Register Repositories
            builder.Services.AddScoped<IBrandRespository, BrandRespository>();

            // Register Services
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IUnitService, UnitService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
  
           

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

            app.Run();
        }
    }
}
