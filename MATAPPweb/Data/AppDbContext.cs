using MATAPPweb.Models;
using Microsoft.EntityFrameworkCore;

namespace MATAPPweb.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ColliderAssignment> ColliderAssignments { get; set; }
    }
}
