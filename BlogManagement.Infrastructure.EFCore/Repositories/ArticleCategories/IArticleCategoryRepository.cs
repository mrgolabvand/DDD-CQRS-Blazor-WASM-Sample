using System.Collections.Generic;
using _0_Framework.Infrastructure;
using BlogManagement.Domain.Aggregates.ArticleCategories;
using BlogManagement.ViewModel.ArticleCategories;

namespace BlogManagement.Infrastructure.EFCore.Repositories.ArticleCategories
{
    public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
    {
        EditArticleCategory? GetDetails(long id);
        Task<List<ArticleCategoryViewModel>> SearchArticleCategoryAsync(string? search);
        Task<List<ArticleCategoryViewModel>> GetAllAsync();
    }
}