using BlogManagement.Domain.Aggregates.ArticleCategories;
using BlogManagement.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Mappings
{
    public class ArticleCategoryMapping : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.ToTable("ArticleCategories");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Title)
                .HasConversion(v => v.Value, v => Title.Create(v).Value)
                .HasMaxLength(Title.MaxLength)
                .IsRequired();

            builder.Property(v => v.Slug)
                .HasConversion(v => v.Value, v => Slug.Create(v).Value)
                .HasMaxLength(Slug.MaxLength)
                .IsRequired();
            
            builder.Property(v => v.Picture)
                .HasConversion(v => v.Value, v => Picture.Create(v).Value)
                .HasMaxLength(Picture.MaxLength)
                .IsRequired();
            
            builder.Property(v => v.PictureAlt)
                .HasConversion(v => v.Value, v => PictureAlt.Create(v).Value)
                .HasMaxLength(PictureAlt.MaxLength)
                .IsRequired();
            
            builder.Property(v => v.PictureTitle)
                .HasConversion(v => v.Value, v => PictureTitle.Create(v).Value)
                .HasMaxLength(PictureTitle.MaxLength)
                .IsRequired();

            builder.HasMany(v => v.Articles)
                .WithOne(v => v.ArticleCategory)
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
