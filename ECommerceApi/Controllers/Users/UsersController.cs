using ECommerceApi.Models.Dtos.Users;
using ECommerceApi.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService service) : ControllerBase
    {
        private readonly IUserService _service = service;

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto loginDto)
        {
            try
            {
                var jwtToken = await _service.Login(loginDto.Username, loginDto.Password);
                return Ok(jwtToken);
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not login: {ex.Message}");
            }
        }

        [HttpPost("registeren")]
        public async Task<ActionResult> Registreren(LoginDto loginDto)
        {
            try
            {
                await _service.Registreren(loginDto);
                return Ok("Succesfully registered a new user");
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not register new user: {ex.Message}");
            }
        }
    }
}
