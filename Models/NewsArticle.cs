namespace Futuristic.Models
{
    public class NewsArticle
    {
        public NewsArticle(string title, string description, ApplicationUser uploader)
        {
            Title = title;
            Description = description;
            Uploader = uploader;
        }
        public NewsArticle()
        {
                
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ApplicationUser Uploader { get; set; }
    }
}
