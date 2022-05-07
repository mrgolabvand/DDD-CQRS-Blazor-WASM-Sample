using BlogManagement.Domain.Aggregates.ArticleCategories;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.ViewModel.Articles
{
    public class CreateArticle
    {
        public IFormFile? Picture { get; set; }
        public string? PictureAlt { get; set; }
        public string? PictureTitle { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }
        public ArticleCategory? ArticleCategory { get; set; }

    }
}
