using BlogManagement.Application.Commands.ArticleCommands;
using BlogManagement.Infrastructure.EFCore.Repositories.Articles;
using Dtat.Result;
using MediatR;
using Resources;
using Resources.Messages;

namespace BlogManagement.Application.CommandHandlers.ArticleCommandHandlers;

public class RestoreArticleCommandHandler : IRequestHandler<RestoreArticleCommand, Dtat.Result.Result>
{
    private readonly IArticleRepository _repository;

    public RestoreArticleCommandHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(RestoreArticleCommand request, CancellationToken cancellationToken)
    {
        var result = new FluentResults.Result();
        
        var isExists = _repository.Exists(v => v.Id == request.id);
        
        if (!isExists)
        {
            var errorMessage = string.Format(Validations.NotFound, DataDictionary.Article);

            result.WithError(errorMessage);

            return result.ConvertToDtatResult();
        }
        
        var articleToRestore = await _repository.GetByIdAsync(request.id);
        
        articleToRestore.Remove();
        
        await _repository.SaveChangesAsync();

        var successMessage = string.Format(Successes.SuccessRestore, DataDictionary.Article);

        result.WithSuccess(successMessage);

        return result.ConvertToDtatResult();
    }
}
