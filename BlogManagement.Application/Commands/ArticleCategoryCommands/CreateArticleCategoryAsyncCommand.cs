using BlogManagement.ViewModel.ArticleCategories;
using MediatR;

namespace BlogManagement.Application.Commands.ArticleCategoryCommands
{
    public record CreateArticleCategoryAsyncCommand(CreateArticleCategory Command) : IRequest<Dtat.Result.Result>; 
}
