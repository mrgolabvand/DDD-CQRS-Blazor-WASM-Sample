using _0_Framework.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.ViewModel.Users;

namespace BlogLand.UI.Server.Controllers.User
{
    [Route("userApi/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthHelper _authHelper;

        public AuthController(IMediator mediator, IAuthHelper authHelper)
        {
            _mediator = mediator;
            _authHelper = authHelper;
        }

        [HttpPost("Register")]
        public async Task<Dtat.Result.Result> Register([FromBody] CreateUser command)
        {
            return await _mediator.Send(new CreateUserAsyncCommand(command));
        }

        [HttpPost("Login")]
        public async Task<Dtat.Result.Result> Login([FromBody] LoginUser command)
        {
            return await _mediator.Send(new LoginAsyncCommand(command));
        }

        [HttpGet("CurrentAccountInfo")]
        public AuthViewModel GetCurrentUserInfo()
        {
            var user = new AuthViewModel();
            if (_authHelper.IsAuthenticated())
            {
                user = _authHelper.CurrentAccountInfo();
            }
            return user;
        }

        [HttpGet("SignOut")]
        public void SignOut()
        {
            if (_authHelper.IsAuthenticated())
            {
                _authHelper.SignOut();
            }
        }
    }
}
