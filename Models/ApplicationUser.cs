using Microsoft.AspNetCore.Identity;

namespace Futuristic.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public IList<NewsArticle> NewsArticles { get; set;}
    }
}
