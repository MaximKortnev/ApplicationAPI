using ApplicationDB.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationDB
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Application> Applications { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            //Database.Migrate();
        }        
    }
}
