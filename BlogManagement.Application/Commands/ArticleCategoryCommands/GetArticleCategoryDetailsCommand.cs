using BlogManagement.ViewModel.ArticleCategories;
using MediatR;

namespace BlogManagement.Application.Commands.ArticleCategoryCommands
{
    public record GetArticleCategoryDetailsCommand(long Id) : IRequest<Dtat.Result.Result<EditArticleCategory>>;
}