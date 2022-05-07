using MediatR;

namespace BlogManagement.Application.Commands.ArticleCommands;

public record RemoveArticleCommand(long id) : IRequest<Dtat.Result.Result>;
