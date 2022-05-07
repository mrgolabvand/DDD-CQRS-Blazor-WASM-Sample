using BlogManagement.Application.Queries.ArticleCategoryQueries;
using BlogManagement.Domain.Aggregates.ArticleCategories;
using BlogManagement.ViewModel.ArticleCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogLand.UI.Server.Controllers.Blog;
[Route("blogApi/[controller]")]
[ApiController]
public class ArticleCategoriesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ArticleCategoriesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAll")]
    public async Task<Dtat.Result.Result<List<ArticleCategoryViewModel>>> GetAll() => await _mediator.Send(new GetAllArticleCategoriesAsyncQuery());
}
