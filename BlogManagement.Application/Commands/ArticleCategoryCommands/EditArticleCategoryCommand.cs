using BlogManagement.ViewModel.ArticleCategories;
using MediatR;

namespace BlogManagement.Application.Commands.ArticleCategoryCommands
{
    public record EditArticleCategoryCommand(EditArticleCategory Command) : IRequest<Dtat.Result.Result>;
}
