using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Dtat.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands;
using UserManagement.Application.Commands.RoleCommands;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Application.Queries;
using UserManagement.ViewModel.Users;

namespace BlogLand.UI.Server.Controllers.User
{
    [Route("userApi/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Search")]
        public async Task<Dtat.Result.Result<List<UserViewModel>>> Search(string? fullName, string? userName, string? phoneNumber, string? email)
        {
            var searchModel = new SearchUserModel
            {
                FullName = fullName,
                UserName = userName,
                PhoneNumber = phoneNumber,
                Email = email
            };
            return await _mediator.Send(new SearchUserAsyncCommand(searchModel));
        }
      
        [HttpGet("GetDetails/{id}")]
        public async Task<Dtat.Result.Result<EditUser>> GetDetails(long id) => await _mediator.Send(new GetDetailsCommand(id));

        [HttpGet("GetChangeRoleDetails/{id}")]
        public async Task<Dtat.Result.Result<ChangeRole>> GetChangeRoleDetails(long id) => await _mediator.Send(new GetChangeRoleCommand(id));

        [HttpPut("Edit")]
        public async Task<Dtat.Result.Result> Edit([FromBody] EditUser editUser) => await _mediator.Send(new EditAsyncCommand(editUser));

        [HttpPut("ChangeRole")]
        public async Task<Dtat.Result.Result> ChangeRole([FromBody] ChangeRole changeRole) => await _mediator.Send(new ChangeRoleCommand(changeRole));

        [HttpPut("ChangePassword")]
        public async Task<Dtat.Result.Result> ChangePassword([FromBody] ChangePassword changePassword) => await _mediator.Send(new ChangePasswordCommand(changePassword));
    }
}
