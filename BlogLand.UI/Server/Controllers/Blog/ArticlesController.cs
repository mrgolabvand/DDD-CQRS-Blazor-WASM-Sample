using BlogManagement.Application.Commands.ArticleCommands;
using BlogManagement.ViewModel.Articles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogLand.UI.Server.Controllers.Blog;

[Route("blogApi/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ArticlesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Search")]
    public async Task<Dtat.Result.Result<List<ArticleViewModel>>> Search(string? search) => await _mediator.Send(new SearchArticleAsyncCommand(search));

    [HttpGet("{id}")]
    public async Task<Dtat.Result.Result<EditArticle>> GetDetails(long id) => await _mediator.Send(new GetArticleDetailsCommand(id));

    [HttpPost("Create")]
    public async Task<Dtat.Result.Result> Create([FromForm] CreateArticle command) => await _mediator.Send(new CreateArticleAsyncCommand(command));

    [HttpPut("Edit")]
    public async Task<Dtat.Result.Result> Edit([FromForm] EditArticle command) => await _mediator.Send(new EditArticleAsyncCommand(command));

    [HttpDelete("Remove/{id}")]
    public async Task<Dtat.Result.Result> Remove(long id) => await _mediator.Send(new RemoveArticleCommand(id));

    [HttpPut("Restore/{id}")]
    public async Task<Dtat.Result.Result> Restore(long id) => await _mediator.Send(new RestoreArticleCommand(id));
}
