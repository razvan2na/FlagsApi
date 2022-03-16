using AutoMapper;
using FlagsApi.Constants;
using FlagsApi.Dtos;
using FlagsApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlagsApi.Controllers
{
    [ApiController]
    [Route("users")]
    [Authorize(Policy = Policies.Viewer)]
    public class UserController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(
            ICountryService countryService,
            IUserService userService,
            ILogger<UserController> logger,
            IMapper mapper)
        {
            _countryService = countryService;
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsers();
                var dtos = users.Select(user => _mapper.Map<UserDto>(user));

                _logger.LogInformation("GET /users: Sending users.");
                return Ok(dtos);
            }
            catch (Exception e)
            {
                _logger.LogError($"GET /users: {e.Message}");
                return Problem(e.Message);
            }
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UserDto>> GetUser(string email)
        {
            try
            {
                var user = await _userService.GetUser(email);

                if (user is null)
                {
                    _logger.LogInformation($"GET /users/{email}: User not found.");
                    return NotFound();
                }

                var role = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (user.Id != id?.Value && role?.Value != Roles.Admin)
                {
                    _logger.LogInformation($"GET /users/{email}: Forbidden.");
                    return Forbid();
                }

                var dto = await _userService.GetUserDto(user);

                _logger.LogInformation($"GET /users/{email}: Sending user.");
                return Ok(dto);
            }
            catch (Exception e)
            {
                _logger.LogError($"GET /users/{email}: {e.Message}");
                return Problem(e.Message);
            }
        }

        [HttpPut("{email}/add/{code}")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> AddCountryToUser(string email, string code)
        {
            try
            {
                var user = await _userService.GetUser(email);

                if (user is null)
                {
                    _logger.LogInformation($"PUT /{email}/add/{code}: Cannot find user.");
                    return NotFound();
                }

                var country = await _countryService.GetCountry(code);

                if (country is null)
                {
                    _logger.LogInformation($"PUT /{email}/add/{code}: Cannot find country.");
                    return NotFound();
                }

                await _userService.AddCountryToUser(user, country);
                _logger.LogInformation($"PUT /{email}/add/{code}: Successfully added country to user.");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"PUT /${email}/add/{code}: {e.Message}");
                return Problem(e.Message);
            }
        }

        [HttpPut("{email}/remove/{code}")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> RemoveCountryFromUser(string email, string code)
        {
            try
            {
                var user = await _userService.GetUser(email);

                if (user is null)
                {
                    _logger.LogInformation($"PUT /{email}/remove/{code}: Cannot find user.");
                    return NotFound();
                }

                var country = await _countryService.GetCountry(code);

                if (country is null)
                {
                    _logger.LogInformation($"PUT /{email}/remove/{code}: Cannot find country");
                    return NotFound();
                }

                await _userService.RemoveCountryFromUser(user, country);
                _logger.LogInformation($"PUT /{email}/remove/{code}: Successfully removed country from user.");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"PUT /{email}/remove/{code}: {e.Message}");
                return Problem(e.Message);
            }
        }
    }
}
