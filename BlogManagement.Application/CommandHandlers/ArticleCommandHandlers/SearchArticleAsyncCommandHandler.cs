using BlogManagement.Application.Commands.ArticleCommands;
using BlogManagement.Infrastructure.EFCore.Repositories.Articles;
using BlogManagement.ViewModel.Articles;
using Dtat.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Application.CommandHandlers.ArticleCommandHandlers
{
    public class SearchArticleAsyncCommandHandler : IRequestHandler<SearchArticleAsyncCommand, Dtat.Result.Result<List<ArticleViewModel>>>
    {
        private readonly IArticleRepository _repository;

        public SearchArticleAsyncCommandHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<ArticleViewModel>>> Handle(SearchArticleAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<List<ArticleViewModel>>();

            var searchResult = await _repository.SearchArticleAsync(request.Search);

            result.WithValue(searchResult);

            return result.ConvertToDtatResult();
        }
    }
}
