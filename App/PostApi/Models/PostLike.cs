using System.ComponentModel.DataAnnotations;

namespace PostApi.Models
{
    public class PostLike
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public DateTime LikedAt { get; set; } = DateTime.UtcNow;
    }
}