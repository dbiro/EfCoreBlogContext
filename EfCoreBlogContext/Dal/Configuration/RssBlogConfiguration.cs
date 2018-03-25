using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreBlogContext.Dal.Configuration
{
    class RssBlogConfiguration : IEntityTypeConfiguration<RssBlog>
    {
        public void Configure(EntityTypeBuilder<RssBlog> builder)
        {            
            builder.Property(rb => rb.RssUrl).IsRequired(true);            
        }
    }
}
