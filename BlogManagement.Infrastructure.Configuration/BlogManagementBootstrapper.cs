using BlogManagement.Application.Commands.ArticleCommands;
using BlogManagement.Infrastructure.EFCore;
using BlogManagement.Infrastructure.EFCore.Repositories.Articles;
using BlogManagement.Infrastructure.EFCore.Repositories.ArticleCategories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.Infrastructure.Configuration
{
    public class BlogManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();

            var assembly = typeof(CreateArticleAsyncCommand).Assembly;
            services.AddMediatR(assembly);

            services.AddDbContext<BlogContext>(v => v.UseSqlServer(connectionString));
        }
    }
}
