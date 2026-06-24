using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using TaskFlowAPI.Services;

namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetoController(ProjetoService projetoService): ControllerBase
    {
        private readonly ProjetoService projetoService = projetoService;
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ProjetoDto>>> GetProjetosAsync()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                List<ProjetoDto> projetos = await projetoService.GetProjetosAsync(userId);
                return projetos;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            } 
        }


        [Authorize]
        [HttpGet("{id:int}")]

        public async Task<ActionResult<ProjetoDto>> GetProjetoById(int id)
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                ProjetoDto projeto = await projetoService.GetProjetoByUserAsync(id,userId);
                return projeto;
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UserProjetoResponseDto>> SetProjetoAsync([FromBody] Projeto projeto)
        {
            try
            {   
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                UserProjetoResponseDto userProjeto = await projetoService.SetUserProjetoAsync(projeto, userId);

                return Created("",userProjeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("{idProjeto}/membro")]
        public async Task<ActionResult> AddMembroProjeto([FromBody] MembroAddDto emailAdd, int idProjeto)
        {

            try
            {
                await projetoService.AddMembroProjetoAsync(emailAdd, idProjeto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.ToString());
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarProjetoAsync(int id)
        {
            try
            {
                await projetoService.DeletarProjetoAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarProjeto(int id, [FromBody] ProjetoUpdateDto projeto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            try
            {
                await projetoService.AtulizarProjeto(id, projeto, userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
