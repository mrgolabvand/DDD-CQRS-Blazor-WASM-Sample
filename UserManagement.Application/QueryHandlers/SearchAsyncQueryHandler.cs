using Dtat.Result;
using MediatR;
using UserManagement.Application.Queries;
using UserManagement.Infrastructure.EFCore.Repositories;
using UserManagement.ViewModel.Users;

namespace UserManagement.Application.QueryHandlers
{
    internal class SearchAsyncQueryHandler : IRequestHandler<SearchAsyncQuery, Dtat.Result.Result<List<UserViewModel>>>
    {
        private readonly IUserQueryRepository _queryRepository;

        public SearchAsyncQueryHandler(IUserQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<Result<List<UserViewModel>>> Handle(SearchAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<List<UserViewModel>>();

            var searchResult = await _queryRepository.SearchAsync(request.query);

            result.WithValue(searchResult);

            return result.ConvertToDtatResult(); ;
        }
    }
}
