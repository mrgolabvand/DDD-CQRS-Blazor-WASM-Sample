using BlogManagement.Application.Commands.ArticleCategoryCommands;
using BlogManagement.Infrastructure.EFCore.Repositories.ArticleCategories;
using BlogManagement.ViewModel.ArticleCategories;
using Dtat.Result;
using MediatR;

namespace BlogManagement.Application.CommandHandlers.ArticleCategoryCommandHandlers
{
    public class SearchArticleCategoryAsyncCommandHandler : IRequestHandler<SearchArticleCategoryAsyncCommand, Dtat.Result.Result<List<ArticleCategoryViewModel>>>
    {
        private readonly IArticleCategoryRepository _repository;

        public SearchArticleCategoryAsyncCommandHandler(IArticleCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<ArticleCategoryViewModel>>> Handle(SearchArticleCategoryAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<List<ArticleCategoryViewModel>>();

            var searchResult = await _repository.SearchArticleCategoryAsync(request.Search);

            result.WithValue(searchResult);

            return result.ConvertToDtatResult();
        }
    }
}
