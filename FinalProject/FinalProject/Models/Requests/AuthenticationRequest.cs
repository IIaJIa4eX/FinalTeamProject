using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Requests
{
    public class AuthenticationRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
