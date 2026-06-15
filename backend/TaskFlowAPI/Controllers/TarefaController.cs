using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using TaskFlowAPI.Services;

namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController (TarefaService tarefaService) : ControllerBase
    {
        private readonly TarefaService tarefaService = tarefaService;
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TarefasPorStatusDto>> ListarTarefasPorStatus(int id)
        {
            try
            {
                TarefasPorStatusDto retornoTarefas = await tarefaService.ListarTarefasPorStatus(id);
                return retornoTarefas;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
               
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TarefaDto>> SetTarefaAsync([FromBody] TarefaDto tarefaDto)
        {
            try
            {
                TarefaDto tarefa = await tarefaService.SetTarefaAsync(tarefaDto);
                return Created();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{idProjeto}/{id}")]
        public async Task<ActionResult> ExcluirTarefaAsync([FromRoute] int idProjeto,[FromRoute] int id)
        {
            try
            {
                await tarefaService.ExcluirTarefaAsync(idProjeto,id);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{idProjeto}/{id}")]
        public async Task<ActionResult> AtualizarTarefaAsync([FromBody] Tarefa tarefa, [FromRoute] int idProjeto, [FromRoute] int id)
        {
            try
            {
                await tarefaService.AtualizarTarefaAsync(tarefa,idProjeto,id);
                return NoContent();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{idTarefa}")]
        public async Task<ActionResult> AtualizarTarefaStatus([FromBody] AtualizarStatusDto statusAtualizado, int idTarefa)
        {
            try
            {
                await tarefaService.AtualizarTarefaStatusAsync(statusAtualizado, idTarefa);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
