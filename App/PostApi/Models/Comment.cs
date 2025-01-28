using System.ComponentModel.DataAnnotations;

namespace PostApi.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int Likes { get; set; } = 0;

        // Powiązanie z modelem Post
        public int PostId { get; set; }

        // Powiązanie z modelem User
        public int UserId { get; set; }
    }
}
