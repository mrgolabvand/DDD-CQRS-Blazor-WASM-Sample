using BlogManagement.ViewModel.ArticleCategories;
using MediatR;

namespace BlogManagement.Application.Commands.ArticleCategoryCommands
{
    public record SearchArticleCategoryAsyncCommand(string? Search): IRequest<Dtat.Result.Result<List<ArticleCategoryViewModel>>>;
}
