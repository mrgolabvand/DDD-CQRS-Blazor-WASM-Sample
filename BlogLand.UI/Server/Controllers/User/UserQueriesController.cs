using _0_Framework.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Queries;
using UserManagement.ViewModel.Users;

namespace BlogLand.UI.Server.Controllers.User
{
    [Route("userQueryApi/[controller]")]
    [ApiController]
    public class UserQueriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserQueriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Search/{value}")]
        public async Task<Dtat.Result.Result<List<UserViewModel>>> Search(string value)
        {
            return await _mediator.Send(new SearchAsyncQuery(value));
        }
    }
}
