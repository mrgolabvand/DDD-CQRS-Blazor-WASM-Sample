using _0_Framework.Application;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.ViewModel.Users;

namespace UserManagement.Application.Commands.UserCommands
{
    public record LoginAsyncCommand(LoginUser command) : IRequest<Dtat.Result.Result>;
}
