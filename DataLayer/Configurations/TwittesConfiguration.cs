using Microsoft.EntityFrameworkCore;
using TwitterServer.DataLayer.Models;

namespace TwitterServer.DataLayer.Configurations
{
    public class TwittesConfiguration : IEntityTypeConfiguration<Twitte>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Twitte> builder)
        {
            builder.Property(x => x.TwitterUserId).IsRequired(true); 
        }
    }
}
