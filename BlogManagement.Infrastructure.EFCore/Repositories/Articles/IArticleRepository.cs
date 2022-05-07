using _0_Framework.Infrastructure;
using BlogManagement.Domain.Aggregates.Articles;
using BlogManagement.ViewModel.Articles;

namespace BlogManagement.Infrastructure.EFCore.Repositories.Articles
{
    public interface IArticleRepository : IRepository<long, Article>
    {
        EditArticle? GetDetails(long id);
        Task<List<ArticleViewModel>> SearchArticleAsync(string? search);
    }
}
