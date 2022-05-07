using BlogManagement.ViewModel.Articles;
using MediatR;

namespace BlogManagement.Application.Commands.ArticleCommands
{
    public record CreateArticleAsyncCommand(CreateArticle Command) : IRequest<Dtat.Result.Result>;
}
