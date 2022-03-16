using System.ComponentModel.DataAnnotations;

namespace FlagsApi.Models
{
    public class Country
    {
        [Key]
        public string Code { get; set; } = "";

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string FlagLink { get; set; } = "";
    }
}
