using Microsoft.EntityFrameworkCore;

namespace Role_AuthDemo.Models
{
    public class LeaveDbContext : DbContext
    {
        public LeaveDbContext(DbContextOptions<LeaveDbContext> options)
            :base(options)
        { 

        }
        public virtual DbSet<User> Users { get; set; }

    }
}
