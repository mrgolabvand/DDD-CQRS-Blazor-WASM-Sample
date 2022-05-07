using BlogManagement.ViewModel.Articles;
using MediatR;

namespace BlogManagement.Application.Commands.ArticleCommands
{
    public record GetArticleDetailsCommand(long Id) : IRequest<Dtat.Result.Result<EditArticle>>;
}
