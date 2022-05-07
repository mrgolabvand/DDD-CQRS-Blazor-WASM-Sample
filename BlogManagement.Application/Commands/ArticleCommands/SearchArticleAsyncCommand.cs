using BlogManagement.ViewModel.Articles;
using MediatR;

namespace BlogManagement.Application.Commands.ArticleCommands
{
    public record SearchArticleAsyncCommand(string? Search) : IRequest<Dtat.Result.Result<List<ArticleViewModel>>>;
}
