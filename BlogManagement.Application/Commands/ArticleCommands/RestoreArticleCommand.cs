using MediatR;

namespace BlogManagement.Application.Commands.ArticleCommands;

public record RestoreArticleCommand(long id) : IRequest<Dtat.Result.Result>;
