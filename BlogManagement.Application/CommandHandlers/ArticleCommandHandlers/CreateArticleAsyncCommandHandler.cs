using _0_Framework.Application;
using BlogManagement.Application.Commands.ArticleCommands;
using BlogManagement.Domain.Aggregates.Articles;
using BlogManagement.Infrastructure.EFCore.Repositories.Articles;
using Dtat.Result;
using MediatR;
using Resources;
using Resources.Messages;

namespace BlogManagement.Application.CommandHandlers.ArticleCommandHandlers
{
    public class CreateArticleAsyncCommandHandler : IRequestHandler<CreateArticleAsyncCommand, Dtat.Result.Result>
    {
        private readonly IFileUploader _fileUploader;
        private readonly IArticleRepository _repository;
        public CreateArticleAsyncCommandHandler(IArticleRepository repository, IFileUploader fileUploader)
        {
            _repository = repository;
            _fileUploader = fileUploader;
        }

        public async Task<Result> Handle(CreateArticleAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result();
            
            var slug = request.Command.Slug.Slugify();
            var picturePath = $"{request.Command.ArticleCategory.Slug}\\{slug}";
            var picture = _fileUploader.Upload(request.Command.Picture, picturePath);
            var article = Article.Create(picture, request.Command.PictureAlt, request.Command.PictureTitle, request.Command.Title, request.Command.ShortDescription, request.Command.Description, slug, request.Command.ArticleCategory);

            result.WithErrors(article.Errors);

            if (article.IsFailed)
                return result.ConvertToDtatResult();

            await _repository.CreateAsync(article.Value);

            var successMessage = string.Format(Successes.SuccessCreate, DataDictionary.Article);

            result.WithSuccess(successMessage);

            return result.ConvertToDtatResult();
        }
    }
}
