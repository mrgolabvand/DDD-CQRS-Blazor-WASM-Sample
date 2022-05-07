using _0_Framework.Infrastructure;
using BlogManagement.Domain.Aggregates.ArticleCategories;
using BlogManagement.ViewModel.ArticleCategories;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repositories.ArticleCategories
{
    public class ArticleCategoryRepository : RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogContext _context;

        public ArticleCategoryRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ArticleCategoryViewModel>> GetAllAsync()
        {
            return await _context.ArticleCategories.Select(v => new ArticleCategoryViewModel
            {
                Id = v.Id,
                Picture = v.Picture.Value,
                PictureAlt = v.PictureAlt.Value,
                PictureTitle = v.PictureTitle.Value,
                Slug = v.Slug.Value,
                Title = v.Title.Value
            }).ToListAsync();
        }

        public EditArticleCategory? GetDetails(long id)
        {
            return _context.ArticleCategories.Select(v => new EditArticleCategory
            {
                Id = v.Id,
                PictureTitle = v.PictureTitle.Value,
                PictureAlt = v.PictureAlt.Value,
                Slug = v.Slug.Value,
                Title = v.Title.Value
            }).FirstOrDefault(v => v.Id == id);
        }

        public async Task<List<ArticleCategoryViewModel>> SearchArticleCategoryAsync(string? search)
        {
            var categories = await _context.ArticleCategories.ToListAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                categories = categories.Where(v => v.Title.Value.Contains(search)).ToList();
            }

            return categories.Select(v => new ArticleCategoryViewModel
            {
                Id = v.Id,
                Picture = v.Picture.Value,
                PictureAlt = v.PictureAlt.Value,
                PictureTitle = v.PictureTitle.Value,
                Slug = v.Slug.Value,
                Title = v.Title.Value
            }).ToList();
        }
    }
}
