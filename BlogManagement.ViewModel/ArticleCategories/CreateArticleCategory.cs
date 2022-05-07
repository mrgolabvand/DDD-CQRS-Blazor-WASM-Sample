using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.ViewModel.ArticleCategories
{
    public class CreateArticleCategory 
    {
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public IFormFile? Picture { get; set; }
        public string? PictureAlt { get; set; }
        public string? PictureTitle { get; set; }
    }
}
