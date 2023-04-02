using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace infnet_bl6_daw_at.Domain.Entities
{
    public class Usuario : IdentityUser
    {
        [
        Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
