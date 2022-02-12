using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterServer.DataLayer.Models;
namespace TwitterServer.DataLayer.Configurations
{
    public class TwitterUserConfiguration : IEntityTypeConfiguration<TwitterUser>
    {
        public void Configure(EntityTypeBuilder<TwitterUser> builder)
        { 
            
        }
    }
}
