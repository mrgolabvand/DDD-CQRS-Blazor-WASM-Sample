using _0_Framework.Application;
using BlogManagement.Application.Commands.ArticleCategoryCommands;
using BlogManagement.Domain.Aggregates.ArticleCategories;
using BlogManagement.Infrastructure.EFCore.Repositories.ArticleCategories;
using Dtat.Result;
using MediatR;
using Resources;
using Resources.Messages;

namespace BlogManagement.Application.CommandHandlers.ArticleCategoryCommandHandlers
{
    public class CreateArticleCategoryAsyncCommandHandler : IRequestHandler<CreateArticleCategoryAsyncCommand, Dtat.Result.Result>
    {
        private readonly IFileUploader _fileUploader;
        private readonly IArticleCategoryRepository _repository;

        public CreateArticleCategoryAsyncCommandHandler(IArticleCategoryRepository repository, IFileUploader fileUploader)
        {
            _repository = repository;
            _fileUploader = fileUploader;
        }

        public async Task<Result> Handle(CreateArticleCategoryAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result();
           
            var picturePath = request.Command.Slug.Slugify();
            var picture = _fileUploader.Upload(request.Command.Picture, picturePath);

            var articleCategory = ArticleCategory.Create(request.Command.Title, request.Command.Slug, picture, request.Command.PictureAlt, request.Command.PictureTitle);

            result.WithErrors(articleCategory.Errors);

            if (articleCategory.IsFailed)
                return result.ConvertToDtatResult();

            await _repository.CreateAsync(articleCategory.Value);

            var successMessage = string.Format(Successes.SuccessCreate, DataDictionary.ArticleCategory);

            result.WithSuccess(successMessage);

            return result.ConvertToDtatResult();
        }
    }
}
