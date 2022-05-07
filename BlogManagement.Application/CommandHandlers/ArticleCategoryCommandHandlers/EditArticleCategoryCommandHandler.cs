using BlogManagement.Application.Commands.ArticleCategoryCommands;
using BlogManagement.Infrastructure.EFCore.Repositories.ArticleCategories;
using Dtat.Result;
using MediatR;
using Resources;
using Resources.Messages;
using _0_Framework.Application;

namespace BlogManagement.Application.CommandHandlers.ArticleCategoryCommandHandlers
{
    public class EditArticleCategoryCommandHandler : IRequestHandler<EditArticleCategoryCommand, Dtat.Result.Result>
    {
        private readonly IArticleCategoryRepository _repository;
        private readonly IFileUploader _fileUploader;
        public EditArticleCategoryCommandHandler(IArticleCategoryRepository repository, IFileUploader fileUploader)
        {
            _repository = repository;
            _fileUploader = fileUploader;
        }

        public async Task<Result> Handle(EditArticleCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result();

            var categoryToEdit = await _repository.GetByIdAsync(request.Command.Id);
            if (categoryToEdit == null)
            {
                var errorMessage = string.Format(Validations.NotFound, DataDictionary.ArticleCategory);

                result.WithError(errorMessage);

                return result.ConvertToDtatResult();
            }
            var slug = request.Command.Slug.Slugify();
            var picturePath = slug;
            var picture = _fileUploader.Upload(request.Command.Picture, picturePath);
            
            var editResult = categoryToEdit.Edit(request.Command.Title, slug, picture, request.Command.PictureAlt, request.Command.PictureTitle);

            if (editResult.IsFailed)
            {
                result.WithErrors(editResult.Errors);
                return result.ConvertToDtatResult();
            }

            await _repository.SaveChangesAsync();

            var successMessage = string.Format(Successes.SuccessUpdate, DataDictionary.ArticleCategory);

            result.WithSuccess(successMessage);

            return result.ConvertToDtatResult();
        }
    }
}