namespace FlagsApi.Dtos
{
    public class UserDto
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public IEnumerable<CountryDto> Countries { get; set; } = new List<CountryDto>();
    }
}
