using BlogManagement.Application.Commands.ArticleCategoryCommands;
using BlogManagement.ViewModel.ArticleCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogLand.UI.Server.Controllers.Blog
{
    [Route("blogApi/[controller]")]
    [ApiController]
    public class ArticleCategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ArticleCategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<Dtat.Result.Result> Create([FromForm] CreateArticleCategory createArticleCategory) => await _mediator.Send(new CreateArticleCategoryAsyncCommand(createArticleCategory));

        [HttpGet("Search")]
        public async Task<Dtat.Result.Result<List<ArticleCategoryViewModel>>> Search(string? search) => await _mediator.Send(new SearchArticleCategoryAsyncCommand(search));
        
        [HttpGet("GetDetails/{id}")]
        public async Task<Dtat.Result.Result<EditArticleCategory>> GetDetails(long id) => await _mediator.Send(new GetArticleCategoryDetailsCommand(id));

        [HttpPut("Edit")]
        public async Task<Dtat.Result.Result> Edit([FromForm] EditArticleCategory command) => await _mediator.Send(new EditArticleCategoryCommand(command));
    }
}
