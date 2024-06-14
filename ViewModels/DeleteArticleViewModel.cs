using Futuristic.Models;

namespace Futuristic.ViewModels
{
    public class DeleteArticleViewModel
    {
        public ApplicationUser currentUser { get; set; }
        public NewsArticle currentArticle { get; set; }
    }
}
