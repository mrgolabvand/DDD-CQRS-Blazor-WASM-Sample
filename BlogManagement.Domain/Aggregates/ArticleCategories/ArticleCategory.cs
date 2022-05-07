using BlogManagement.Domain.Aggregates.Articles;
using BlogManagement.Domain.SharedKernel;

namespace BlogManagement.Domain.Aggregates.ArticleCategories
{
    public class ArticleCategory
    {
        public long Id { get; private set; }
        public Title Title { get; private set; }
        public Slug Slug { get; private set; }
        public Picture Picture { get; private set; }
        public PictureAlt PictureAlt { get; private set; }
        public PictureTitle PictureTitle { get; private set; }
        private readonly List<Article> _articles;
        public virtual IReadOnlyList<Article> Articles { get { return _articles; } }

        private ArticleCategory() : base()
        {
            _articles = new List<Article>();
        }
        public ArticleCategory(Title title, Slug slug, Picture picture, PictureAlt pictureAlt, PictureTitle pictureTitle)
        {
            Title = title;
            Slug = slug;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
        }

        public static FluentResults.Result<ArticleCategory> Create(string? title, string? slug, string? picture, string? pictureAlt, string? pictureTitle)
        {
            var result = new FluentResults.Result<ArticleCategory>();

            var titleResult = Title.Create(title);
            result.WithErrors(titleResult.Errors);

            var slugResult = Slug.Create(slug);
            result.WithErrors(slugResult.Errors);
            
            var pictureResult = Picture.Create(picture);
            result.WithErrors(pictureResult.Errors);

            var pictureAltResult = PictureAlt.Create(pictureAlt);
            result.WithErrors(pictureAltResult.Errors);

            var pictureTitleResult = PictureTitle.Create(pictureTitle);
            result.WithErrors(pictureTitleResult.Errors);

            if (result.IsFailed)
                return result;

            var returnValue = new ArticleCategory(titleResult.Value, slugResult.Value, pictureResult.Value, pictureAltResult.Value, pictureTitleResult.Value);

            result.WithValue(returnValue);

            return result;
        }

        public FluentResults.Result Edit(string? title, string? slug, string? picture, string? pictureAlt, string? pictureTitle)
        {
            var result = new FluentResults.Result<ArticleCategory>();

            var titleResult = Title.Create(title);
            result.WithErrors(titleResult.Errors);

            var slugResult = Slug.Create(slug);
            result.WithErrors(slugResult.Errors);

            var pictureResult = new FluentResults.Result<Picture>();

            if (!string.IsNullOrWhiteSpace(picture))
            {
                pictureResult = Picture.Create(picture);
                result.WithErrors(pictureResult.Errors);
            }

            var pictureAltResult = PictureAlt.Create(pictureAlt);
            result.WithErrors(pictureAltResult.Errors);

            var pictureTitleResult = PictureTitle.Create(pictureTitle);
            result.WithErrors(pictureTitleResult.Errors);
            
            if (result.IsFailed)
                return result.ToResult();
          
            var returnValue = new ArticleCategory(titleResult.Value, slugResult.Value, pictureResult.Value, pictureAltResult.Value, pictureTitleResult.Value);

            result.WithValue(returnValue);
            Title = result.Value.Title;
            Slug = result.Value.Slug;
    
            if (!string.IsNullOrWhiteSpace(picture))
            {
                Picture = result.Value.Picture;
            }
    
            PictureAlt = result.Value.PictureAlt;
            PictureTitle = result.Value.PictureTitle;

            return result.ToResult();
        }
    }
}
