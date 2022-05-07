using BlogManagement.Domain.Aggregates.ArticleCategories;
using BlogManagement.Domain.Aggregates.Articles.ValueObjects;
using BlogManagement.Domain.SharedKernel;
using Resources;
using Resources.Messages;

namespace BlogManagement.Domain.Aggregates.Articles
{
    public class Article
    {
        public long Id { get; private set; }
        public Picture Picture { get; private set; }
        public PictureAlt PictureAlt { get; private set; }
        public PictureTitle PictureTitle { get; private set; }
        public Title Title { get; private set; }
        public ShortDescription ShortDescription { get; private set; }
        public Description Description { get; private set; }
        public Slug Slug { get; private set; }
        public bool IsRemoved { get; private set; } = false;
        public virtual ArticleCategory ArticleCategory { get; private set; }

        private Article() : base()
        {
        }
        public Article(Picture picture, PictureAlt pictureAlt, PictureTitle pictureTitle, Title title, ShortDescription shortDescription, Description description, Slug slug, ArticleCategory articleCategory)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Title = title;
            ShortDescription = shortDescription;
            Description = description;
            Slug = slug;
            ArticleCategory = articleCategory;
        }

        public static FluentResults.Result<Article> Create(string? picture, string? pictureAlt, string? pictureTitle, string? title, string? shortDescription, string? description, string? slug, ArticleCategory? articleCategory)
        {
            var result = new FluentResults.Result<Article>();

            var pictureResult = Picture.Create(picture);
            result.WithErrors(pictureResult.Errors);

            var pictureAltResult = PictureAlt.Create(pictureAlt);
            result.WithErrors(pictureAltResult.Errors);

            var pictureTitleResult = PictureTitle.Create(pictureTitle);
            result.WithErrors(pictureTitleResult.Errors);

            var titleResult = Title.Create(title);
            result.WithErrors(titleResult.Errors);

            var shortDescriptionResult = ShortDescription.Create(shortDescription);
            result.WithErrors(shortDescriptionResult.Errors);

            var descriptionResult = Description.Create(description);
            result.WithErrors(descriptionResult.Errors);

            var slugResult = Slug.Create(slug);
            result.WithErrors(slugResult.Errors);

            if (articleCategory is null)
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.ArticleCategory);

                result.WithError(errorMessage);
            }

            if (result.IsFailed)
                return result;

            var returnValue = new Article(pictureResult.Value, pictureAltResult.Value, pictureTitleResult.Value, titleResult.Value, shortDescriptionResult.Value, descriptionResult.Value, slugResult.Value, articleCategory);

            result.WithValue(returnValue);

            return result;
        }

        public FluentResults.Result Edit(string? picture, string? pictureAlt, string? pictureTitle, string? title, string? shortDescription, string? description, string? slug)
        {
            var result = Create(picture, pictureAlt, pictureTitle, title, shortDescription, description, slug, ArticleCategory);

            if (result.IsFailed)
                return result.ToResult();

            Picture = result.Value.Picture;
            PictureAlt = result.Value.PictureAlt;
            PictureTitle = result.Value.PictureTitle;
            Title = result.Value.Title;
            ShortDescription = result.Value.ShortDescription;
            Description = result.Value.Description;
            Slug = result.Value.Slug;

            return result.ToResult();
        }

        public FluentResults.Result EditCategory(ArticleCategory articleCategory)
        {
            var result = Create(Picture.Value, PictureAlt.Value, PictureTitle.Value, Title.Value, ShortDescription.Value, Description.Value, Slug.Value, articleCategory);

            if (result.IsFailed)
                return result.ToResult();

            ArticleCategory = articleCategory;

            return result.ToResult();
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
