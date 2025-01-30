using System.ComponentModel.DataAnnotations;

namespace PostApi.Models
{
    public class Post
    {

        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int Likes { get; set; } = 0;
        public string UserId { get; set; } = string.Empty; // Powiązanie z użytkownikiem w IdentityApi

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
