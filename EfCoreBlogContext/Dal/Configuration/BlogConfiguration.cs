using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreBlogContext.Dal.Configuration
{
    class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable(nameof(Blog));            
            builder.Property(b => b.Url);                
            builder.Property(b => b.Rating).HasColumnType("decimal(18,8)");
            builder.HasDiscriminator<int>("Type")
                .HasValue<Blog>((int)BlogType.Blog)
                .HasValue<RssBlog>((int)BlogType.RssBlog);
        }
    }
}
