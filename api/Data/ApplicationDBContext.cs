using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) 
            : base(dbContextOptions)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Link> Links { get; set; }
    }
}
