using AutoMapper;
using FlagsApi.Constants;
using FlagsApi.Dtos;
using FlagsApi.Models;
using FlagsApi.Services;
using FlagsApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlagsApi.Controllers
{
    [ApiController]
    [Route("countries")]
    [Authorize(Policy = Policies.Viewer)]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(ICountryService countryService, 
            ILogger<CountryController> logger, IMapper mapper)
        {
            _countryService = countryService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries()
        {
            try
            {
                var user = HttpContext.User;

                var countries = await _countryService.GetCountries();
                var dtos = _mapper.Map<IEnumerable<CountryDto>>(countries);

                _logger.LogInformation("GET /countries: Sending countries.");
                return Ok(dtos);
            }
            catch (Exception e)
            {
                _logger.LogError($"GET /countries: {e.Message}");
                return Problem(e.Message);
            }
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<CountryDto>> GetCountry(string code)
        {
            try
            {
                var country = await _countryService.GetCountry(code);

                if (country is null)
                {
                    _logger.LogInformation($"GET /countries/{code}: Couldn't find country.");
                    return NotFound();
                }

                var dto = _mapper.Map<CountryDto>(country);

                _logger.LogInformation($"GET /countries/{code}: Sending country.");
                return Ok(country);
            }
            catch (Exception e)
            {
                _logger.LogError($"GET /countries/{code}: {e.Message}");
                return Problem(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> AddCountry([FromBody] CountryDto dto)
        {
            try
            {
                var country = _mapper.Map<Country>(dto);

                await _countryService.AddCountry(country);

                _logger.LogInformation("POST /countries: Successfully added country.");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"POST /countries: {e.Message}");
                return Problem(e.Message);
            }
        }

        [HttpPut("{code}")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> UpdateCountry(string code, [FromBody] CountryDto dto)
        {
            try
            {
                if (code != dto.Code)
                {
                    _logger.LogInformation($"PUT /countries/{code}: Codes do not match.");
                    return BadRequest("Codes do not match.");
                }

                if (await _countryService.GetCountry(code) is null)
                {
                    _logger.LogInformation($"PUT /countries/{code}: Couldn't find country.");
                    return NotFound();
                }

                var country = _mapper.Map<Country>(dto);

                await _countryService.UpdateCountry(country);

                _logger.LogInformation($"PUT /countries/{code}: Successfully updated country.");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"PUT /countries/{code}: {e.Message}");
                return Problem(e.Message);
            }
        }

        [HttpDelete("{code}")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> DeleteCountry(string code)
        {
            try
            {
                var country = await _countryService.GetCountry(code);

                if (country is null)
                {
                    _logger.LogInformation($"DELETE /countries/{code}: Couldn't find country.");
                    return NotFound();
                }

                await _countryService.DeleteCountry(country);

                _logger.LogInformation($"DELETE /countries/{code}: Successfully deleted country.");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"DELETE /countries: {e.Message}");
                return Problem(e.Message);
            }
        }
    }
}
