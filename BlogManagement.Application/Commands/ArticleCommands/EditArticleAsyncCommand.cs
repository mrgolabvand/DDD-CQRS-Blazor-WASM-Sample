using BlogManagement.ViewModel.Articles;
using MediatR;

namespace BlogManagement.Application.Commands.ArticleCommands
{
    public record EditArticleAsyncCommand(EditArticle Command) : IRequest<Dtat.Result.Result>;
}