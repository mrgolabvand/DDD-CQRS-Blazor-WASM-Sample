using BlogManagement.Application.Commands.ArticleCategoryCommands;
using BlogManagement.ViewModel.ArticleCategories;
using Dtat.Result;
using MediatR;
using BlogManagement.Infrastructure.EFCore.Repositories.ArticleCategories;
using Resources.Messages;
using Resources;

namespace BlogManagement.Application.CommandHandlers.ArticleCategoryCommandHandlers
{
    public class GetArticleCategoryDetailsCommandHnadler : IRequestHandler<GetArticleCategoryDetailsCommand, Dtat.Result.Result<EditArticleCategory>>
    {
        private readonly IArticleCategoryRepository _repository;

        public GetArticleCategoryDetailsCommandHnadler(IArticleCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<EditArticleCategory>> Handle(GetArticleCategoryDetailsCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<EditArticleCategory>();

            var isExists = _repository.Exists(v => v.Id == request.Id);
            if (!isExists)
            {
                var errorMessage = string.Format(Validations.NotFound, DataDictionary.ArticleCategory);

                result.WithError(errorMessage);

                return result.ConvertToDtatResult();
            }

            var category = _repository.GetDetails(request.Id);

            result.WithValue(category);

            return result.ConvertToDtatResult();
        }
    }
}