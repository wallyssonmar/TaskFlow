using Microsoft.AspNetCore.Mvc;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using TaskFlowAPI.Services;

namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetoController(ProjetoService taskFlowService): ControllerBase
    {
        private readonly ProjetoService taskFlowService = taskFlowService;

        [HttpGet]
        public async Task<ActionResult<List<ProjetoDto>>> GetProjetosAsync()
        {
            try
            {
                List<ProjetoDto> projetos = await taskFlowService.GetProjetosAsync();
                return projetos;
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
            
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<ProjetoDto>> GetProjetoById(int id)
        {
            try
            {
                ProjetoDto projeto = await taskFlowService.GetProjetoByIdAsync(id);
                return projeto;
            }
            catch (KeyNotFoundException ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Projeto>> SetProjetoAsync([FromBody] ProjetoDto projeto)
        {
            try
            {
                ProjetoDto RetornoprojetoDto = await taskFlowService.SetProjetoAsync(projeto);
                return Created("",RetornoprojetoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
            
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeletarProjetoAsync(int id)
        {
            try
            {
                await taskFlowService.DeletarProjetoAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarProjeto(int id, [FromBody] ProjetoUpdateDto projeto)
        {
            try
            {
                await taskFlowService.AtulizarProjeto(id, projeto);
                return NoContent();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
    }
}
