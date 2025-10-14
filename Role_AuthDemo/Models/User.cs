using System.ComponentModel.DataAnnotations;

namespace Role_AuthDemo.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        //Manager or Employee 
        public string Role { get; set; } = null!;
    }
}
