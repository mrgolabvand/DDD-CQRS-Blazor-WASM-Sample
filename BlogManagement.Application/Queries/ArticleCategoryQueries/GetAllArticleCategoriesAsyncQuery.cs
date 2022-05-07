using BlogManagement.Domain.Aggregates.ArticleCategories;
using BlogManagement.ViewModel.ArticleCategories;
using MediatR;

namespace BlogManagement.Application.Queries.ArticleCategoryQueries;

public record GetAllArticleCategoriesAsyncQuery() : IRequest<Dtat.Result.Result<List<ArticleCategoryViewModel>>>;
