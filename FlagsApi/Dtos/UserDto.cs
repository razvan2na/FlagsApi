namespace FlagsApi.Dtos
{
    public class UserDto
    {
        public string Username { get; set; } = "";

        public string Email { get; set; } = "";

        public IEnumerable<CountryDto> Countries { get; set; } = new List<CountryDto>();
    }
}
