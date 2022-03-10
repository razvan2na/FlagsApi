using Microsoft.AspNetCore.Identity;

namespace FlagsApi.Models
{
    public class User : IdentityUser
    {
        public ICollection<Country> Countries { get; set; } = new List<Country>();
    }
}
