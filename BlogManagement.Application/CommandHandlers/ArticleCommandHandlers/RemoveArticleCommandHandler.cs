using BlogManagement.Application.Commands.ArticleCommands;
using BlogManagement.Infrastructure.EFCore.Repositories.Articles;
using Dtat.Result;
using MediatR;
using Resources;
using Resources.Messages;

namespace BlogManagement.Application.CommandHandlers.ArticleCommandHandlers;

public class RemoveArticleCommandHandler : IRequestHandler<RemoveArticleCommand, Dtat.Result.Result>
{
    private readonly IArticleRepository _repository;

    public RemoveArticleCommandHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(RemoveArticleCommand request, CancellationToken cancellationToken)
    {
        var result = new FluentResults.Result();
        
        var isExists = _repository.Exists(v => v.Id == request.id);
        
        if (!isExists)
        {
            var errorMessage = string.Format(Validations.NotFound, DataDictionary.Article);

            result.WithError(errorMessage);

            return result.ConvertToDtatResult();
        }
        
        var articleToRemove = await _repository.GetByIdAsync(request.id);
        
        articleToRemove.Remove();
        
        await _repository.SaveChangesAsync();

        var successMessage = string.Format(Successes.SuccessDelete, DataDictionary.Article);

        result.WithSuccess(successMessage);

        return result.ConvertToDtatResult();
    }
}
