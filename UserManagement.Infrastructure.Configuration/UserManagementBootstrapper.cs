using _0_Framework.Application;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Commands;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Infrastructure.EFCore;
using UserManagement.Infrastructure.EFCore.Repositories;

namespace UserManagement.Infrastructure.Configuration
{
    public class UserManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserQueryRepository, UserQueryRepository>();

            var assembly = typeof(CreateUserAsyncCommand).Assembly;
            services.AddMediatR(assembly);

            services.AddDbContext<UserContext>(v => v.UseSqlServer(connectionString));
        }

    }
}
