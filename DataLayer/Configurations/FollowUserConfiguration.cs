using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterServer.DataLayer.Models;

namespace TwitterServer.DataLayer.Configurations
{
    public class FollowUserConfiguration : IEntityTypeConfiguration<FollowUser>
    {
        public void Configure(EntityTypeBuilder<FollowUser> builder)
        {
            builder.HasKey(bc => new { bc.FollowerUserId, bc.TwitterUserId });
            builder.HasOne(bc => bc.TwitterUser).WithMany(b => b.Followers).HasForeignKey(bc => bc.TwitterUserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(bc => bc.FollowerUser).WithMany(c => c.Following).HasForeignKey(bc => bc.FollowerUserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
