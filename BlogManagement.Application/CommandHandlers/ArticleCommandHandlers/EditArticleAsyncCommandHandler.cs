using _0_Framework.Application;
using BlogManagement.Application.Commands.ArticleCommands;
using BlogManagement.Infrastructure.EFCore.Repositories.Articles;
using Dtat.Result;
using MediatR;
using Resources;
using Resources.Messages;

namespace BlogManagement.Application.CommandHandlers.ArticleCommandHandlers
{
    public class EditArticleAsyncCommandHandler : IRequestHandler<EditArticleAsyncCommand, Dtat.Result.Result>
    {
        private readonly IArticleRepository _repository;
        private readonly IFileUploader _fileUploader;
        public EditArticleAsyncCommandHandler(IArticleRepository repository, IFileUploader fileUploader)
        {
            _repository = repository;
            _fileUploader = fileUploader;
        }

        public async Task<Result> Handle(EditArticleAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result();

            var articleToEdit = await _repository.GetByIdAsync(request.Command.Id);
            if (articleToEdit == null)
            {
                var errorMessage = string.Format(Validations.NotFound, DataDictionary.Article);

                result.WithError(errorMessage);

                return result.ConvertToDtatResult();
            }
            var slug = request.Command.Slug.Slugify();
            var picturePath = $"{request.Command.ArticleCategory.Slug}\\{slug}";
            var picture = _fileUploader.Upload(request.Command.Picture, picturePath);

            var editResult = articleToEdit.Edit(picture, request.Command.PictureAlt, request.Command.PictureTitle, request.Command.Title, request.Command.ShortDescription, request.Command.Description, slug);

            if (editResult.IsFailed)
            {
                result.WithErrors(editResult.Errors);
                return result.ConvertToDtatResult();
            }

            await _repository.SaveChangesAsync();

            var successMessage = string.Format(Successes.SuccessUpdate, DataDictionary.Article);

            result.WithSuccess(successMessage);

            return result.ConvertToDtatResult();
        }
    }
}