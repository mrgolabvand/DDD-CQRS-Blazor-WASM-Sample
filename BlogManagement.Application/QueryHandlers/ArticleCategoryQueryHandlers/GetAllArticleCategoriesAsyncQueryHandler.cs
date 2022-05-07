using BlogManagement.Application.Queries.ArticleCategoryQueries;
using BlogManagement.Domain.Aggregates.ArticleCategories;
using BlogManagement.Infrastructure.EFCore.Repositories.ArticleCategories;
using BlogManagement.ViewModel.ArticleCategories;
using Dtat.Result;
using MediatR;
using Resources;
using Resources.Messages;

namespace BlogManagement.Application.QueryHandlers.ArticleCategoryQueryHandlers;
public class GetAllArticleCategoriesAsyncQueryHandler : IRequestHandler<GetAllArticleCategoriesAsyncQuery, Dtat.Result.Result<List<ArticleCategoryViewModel>>>
{
    private readonly IArticleCategoryRepository _repository;

    public GetAllArticleCategoriesAsyncQueryHandler(IArticleCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<ArticleCategoryViewModel>>> Handle(GetAllArticleCategoriesAsyncQuery request, CancellationToken cancellationToken)
    {
        var result = new FluentResults.Result<List<ArticleCategoryViewModel>>();

        var articleCategories = await _repository.GetAllAsync();
        if (articleCategories == null)
        {
            var errorMessage = string.Format(Validations.NotFound, DataDictionary.ArticleCategory);

            result.WithError(errorMessage);

            return result.ConvertToDtatResult();
        }

        result.WithValue(articleCategories);

        return result.ConvertToDtatResult();
    }
}
