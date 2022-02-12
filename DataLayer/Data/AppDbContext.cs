using Microsoft.EntityFrameworkCore;
using TwitterServer.DataLayer.Models;
using TwitterServer.DataLayer.Configurations;

namespace TwitterServer.Data
{
    public class AppDbContext : DbContext
    {
        public  DbSet<TwitterUser> TwitterUsers { get; set; }
        public  DbSet<Retweet> Retweets { get; set; }
      //  public  DbSet<FollowerUser> FollowerUser { get;set;}
        public DbSet<FollowUser> FollowUser { get; set; }

        public DbSet<Twitte> Twittes { get;set;}

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TwittesConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FollowUserConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TwitterUserConfiguration).Assembly); 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReTwitteConfiguration).Assembly);
        }
    }
}