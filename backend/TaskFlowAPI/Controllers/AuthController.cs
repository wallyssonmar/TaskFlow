using Microsoft.AspNetCore.Mvc;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using TaskFlowAPI.Services;

namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthService authService): ControllerBase
    {

        private readonly AuthService authService = authService;

        [HttpPost("register")]
        public async Task<ActionResult> CriarUserAsync([FromBody] RegisterDto user)
        {
            try
            {
                await authService.CriarUserAsync(user);
                return Created();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]

        public async Task<ActionResult> VerificarLoginAsync([FromBody] LoginDto loginDto)
        {
            try
            {
                await authService.VerificarLoginAsync(loginDto);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
