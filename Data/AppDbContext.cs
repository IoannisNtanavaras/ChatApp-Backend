using Microsoft.EntityFrameworkCore;
using BACKEND.Models;

namespace BACKEND.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}