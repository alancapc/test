using Microsoft.EntityFrameworkCore;

namespace test.Models
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Blogger> Bloggers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=ALAN\\ALANSQLSERVER;Database=test; Integrated Security=true");
        }
    }
}