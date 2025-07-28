using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CarRental.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) 
            : base(options)
        {
            
        }
    }
}
