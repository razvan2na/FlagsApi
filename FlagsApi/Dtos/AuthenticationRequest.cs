using System.ComponentModel.DataAnnotations;

namespace FlagsApi.Dtos
{
    public class AuthenticationRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
