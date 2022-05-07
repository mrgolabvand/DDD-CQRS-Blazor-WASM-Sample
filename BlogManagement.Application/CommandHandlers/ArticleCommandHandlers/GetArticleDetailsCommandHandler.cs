using BlogManagement.Application.Commands.ArticleCommands;
using BlogManagement.Infrastructure.EFCore.Repositories.Articles;
using BlogManagement.ViewModel.Articles;
using Dtat.Result;
using MediatR;
using Resources;
using Resources.Messages;

namespace BlogManagement.Application.CommandHandlers.ArticleCommandHandlers
{
    public class GetArticleDetailsCommandHandler : IRequestHandler<GetArticleDetailsCommand, Dtat.Result.Result<EditArticle>>
    {
        private readonly IArticleRepository _repository;

        public GetArticleDetailsCommandHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<EditArticle>> Handle(GetArticleDetailsCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<EditArticle>();

            var isExists = _repository.Exists(v => v.Id == request.Id);
            if (!isExists)
            {
                var errorMessage = string.Format(Validations.NotFound, DataDictionary.Article);

                result.WithError(errorMessage);

                return result.ConvertToDtatResult();
            }

            var article = _repository.GetDetails(request.Id);

            result.WithValue(article);

            return result.ConvertToDtatResult();
        }
    }
}