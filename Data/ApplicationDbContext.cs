using Futuristic.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Futuristic.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public object ApplicationUser { get; internal set; }
        public DbSet<ApplicationUser> applicationUsers {  get; set; }  
        public DbSet<NewsArticle> articles { get; set; }
        
    }
}