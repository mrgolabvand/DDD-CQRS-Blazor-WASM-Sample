using _0_Framework.Infrastructure;
using BlogManagement.Domain.Aggregates.Articles;
using BlogManagement.ViewModel.Articles;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repositories.Articles
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {
        private readonly BlogContext _context;

        public ArticleRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public EditArticle? GetDetails(long id)
        {
            return _context.Articles.Select(v => new EditArticle
            {
                Id = v.Id,
                Description = v.Description.Value,
                PictureAlt = v.PictureAlt.Value,
                PictureTitle = v.PictureTitle.Value,
                ShortDescription = v.ShortDescription.Value,
                Slug = v.Slug.Value,
                Title = v.Title.Value
            }).FirstOrDefault(v => v.Id == id);
        }

        public async Task<List<ArticleViewModel>> SearchArticleAsync(string? search)
        {
            var articles = await _context.Articles.Include(v => v.ArticleCategory).ToListAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                articles = articles.Where(v => v.Title.Value.Contains(search) || v.ShortDescription.Value.Contains(search)).ToList();
            }

            return articles.Select(v => new ArticleViewModel
            {
                Id = v.Id,
                Picture = v.Picture.Value,
                PictureAlt = v.PictureAlt.Value,
                PictureTitle = v.PictureTitle.Value,
                Slug = v.Slug.Value,
                Title = v.Title.Value,
                ArticleCategory = v.ArticleCategory,
                Description = v.Description.Value,
                IsRemoved = v.IsRemoved,
                ShortDescription = v.ShortDescription.Value
            }).ToList();
        }
    }
}
