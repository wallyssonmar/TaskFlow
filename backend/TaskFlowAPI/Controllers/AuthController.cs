using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlowAPI.Authentication;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using TaskFlowAPI.Services;

namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthService authService, TokenService tokenService): ControllerBase
    {

        private readonly AuthService authService = authService;
        private readonly TokenService tokenService = tokenService;

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
        public async Task<ActionResult<LoginResponse>> VerificarLoginAsync([FromBody] LoginDto loginDto)
        {
            try
            {
                LoginResponse token = await authService.VerificarLoginAsync(loginDto);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<LoginResponse>> RefreshTokenAsync([FromBody] RefreshTokenResponseDto refreshTokenResponse)
        {
            if (refreshTokenResponse is null)
                 return BadRequest();

            LoginResponse response = await authService.RefreshTokenAsync(refreshTokenResponse.RefreshToken);

            if(response is null)
                return Unauthorized();

            return Ok(response);
            
        }
            

        [Authorize]
        [HttpGet("teste")]
        public IActionResult Teste()
        {
            return Ok("Você está autenticado!");
        }
    }
}
