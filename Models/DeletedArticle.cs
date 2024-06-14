using System.ComponentModel.DataAnnotations;

namespace Futuristic.Models
{
    
    public class DeletedArticle
    {
        public int Id { get; set; }
        public NewsArticle NewsArticle { get; set; }
        public string reasonToDelete { get; set; }
    }
}
