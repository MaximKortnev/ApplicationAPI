using Application.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    /// <summary>
    /// Контекст базы данных для работы с заявками.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Коллекция заявок в базе данных.
        /// </summary>
        public DbSet<ApplicationDBModel> Applications { get; set; }

        /// <summary>
        /// Конструктор контекста базы данных.
        /// </summary>
        /// <param name="options">Параметры контекста базы данных.</param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            //Database.Migrate();
        }
    }
}