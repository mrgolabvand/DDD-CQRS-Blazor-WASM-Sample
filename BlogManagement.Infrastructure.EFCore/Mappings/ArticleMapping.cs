using BlogManagement.Domain.Aggregates.Articles;
using BlogManagement.Domain.Aggregates.Articles.ValueObjects;
using BlogManagement.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Mappings
{
    public class ArticleMapping : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");

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

            builder.Property(v => v.ShortDescription)
                .HasConversion(v => v.Value, v => ShortDescription.Create(v).Value)
                .HasMaxLength(ShortDescription.MaxLength)
                .IsRequired();

            builder.Property(v => v.IsRemoved);

            builder.Property(v => v.Description)
                .HasConversion(v => v.Value, v => Description.Create(v).Value)
                .HasMaxLength(Description.MaxLength)
                .IsRequired();

            builder.HasOne(v => v.ArticleCategory)
                .WithMany(v => v.Articles)
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
