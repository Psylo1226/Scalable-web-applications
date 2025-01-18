using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace IdentityApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public string? Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
