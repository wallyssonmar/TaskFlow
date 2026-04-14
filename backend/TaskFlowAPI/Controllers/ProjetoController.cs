using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<List<Projeto>>> GetProjetosAsync()
        {
            try
            {
                List<Projeto> projetos = await taskFlowService.GetProjetosAsync();
                return projetos;
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<Projeto>> SetProjetoAsync([FromBody] Projeto projeto)
        {
            try
            {
                Projeto Retornoprojeto = await taskFlowService.SetProjetoAsync(projeto);
                return Created("",Retornoprojeto);
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
        public async Task<ActionResult> AtualizarProjeto(int id, [FromBody] Projeto projeto)
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
