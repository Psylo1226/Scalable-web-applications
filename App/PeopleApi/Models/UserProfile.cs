using System.ComponentModel.DataAnnotations;

namespace PeopleApi.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty; // Powiązanie z użytkownikiem w IdentityApi
    }
}
