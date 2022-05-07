using _0_Framework;
using _0_Framework.Application;
using Dtat.Result;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Resources;
using Resources.Messages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserManagement.Application.Commands;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Infrastructure.EFCore.Repositories;

namespace UserManagement.Application.CommandHandlers.UserCommandHandlers
{
    public class LoginAsyncCommandHandler : IRequestHandler<LoginAsyncCommand, Dtat.Result.Result>
    {
        private readonly IAuthHelper _authHelper;
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public LoginAsyncCommandHandler(IUserRepository repository, IPasswordHasher passwordHasher, IAuthHelper authHelper)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _authHelper = authHelper;
        }

        public async Task<Result> Handle(LoginAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<AuthViewModel>();

            var account = await _repository.GetbyUserNameAsync(request.command.UserName);

            if (account == null)
            {
                var errorMessage = string.Format(Validations.UserNameOrPasswordIsWrong);

                result.WithError(errorMessage);
                
                return result.ConvertToDtatResult();
            }

            (bool Verified, bool NeedsUpgrade) passwordResult = _passwordHasher.Check(account.Password.Value, request.command.Password);

            if (!passwordResult.Verified)
            {
                var errorMessage = string.Format(Validations.UserNameOrPasswordIsWrong);

                result.WithError(errorMessage);

                return result.ConvertToDtatResult();
            }

            var authViewModel = new AuthViewModel(account.Id, account.UserName.Value, account.Role.Value);
             
            //Cookie Auth
            _authHelper.Signin(authViewModel);
            
            //Jwt Auth
            //var token = Tools.CreateToken(authViewModel);
            
            var successMessage = string.Format(Successes.SuccessLogin);

            result.WithSuccess(successMessage);
            return result.ConvertToDtatResult();
        }

    }
}
