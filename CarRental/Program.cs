using CarRental.Data;
using CarRental.Models;
using CarRental.repo.Implementations;
using CarRental.repo.Interfaces;
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

            // Register DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CarRentalDbConnection")));

            // Register Repositories
            builder.Services.AddScoped<IBrandRespository, BrandRespository>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IUnitRepository, UnitRepository>();
            builder.Services.AddScoped<IUserRespository, UserRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IRequestRepository, RequestRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<IDashBoardRepository, DashboardRepository>();

            // Register Services
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IUnitService, UnitService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            var app = builder.Build();

            // Configure middleware
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
                pattern: "{controller=Home}/{action=Index}/{id?}");



            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Ensure database is created
                context.Database.EnsureCreated();

                // Check if admin user exists
                var adminExists = context.Users.Any(u => u.UserName == "admin123");

                if (!adminExists)
                {
                    var adminUser = new User
                    {
                        Name = "Admin",
                        Email = "admin@carrental.com",
                        UserName = "Admin123",
                        Password = "7nmXbJOA1eM3/BwJXs6MjyL5HzBs7rFh+lH+zt4sS6E=",
                        Role = 0,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now
                    };

                    context.Users.Add(adminUser);
                    context.SaveChanges();
                }
            }


            //(localdb)\MSSQLLocalDb











            app.Run();
        }
    }
}

    

