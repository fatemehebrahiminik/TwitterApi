using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterServer.DataLayer.Models;
namespace TwitterServer.DataLayer.Configurations
{
    public class ReTwitteConfiguration : IEntityTypeConfiguration<Retweet>
    {
        public void Configure(EntityTypeBuilder<Retweet> builder)
        { 
        }
    }
}
