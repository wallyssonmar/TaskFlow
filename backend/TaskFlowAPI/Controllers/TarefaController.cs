using Microsoft.AspNetCore.Mvc;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Services;

namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController (TarefaService tarefaService) : ControllerBase
    {
        private readonly TarefaService tarefaService = tarefaService;
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

        [HttpPost]
        public async Task<ActionResult<TarefaDto>> SetTarefaAsync([FromBody] TarefaDto tarefaDto)
        {
            try
            {
                TarefaDto tarefa = await tarefaService.SetTarefaAsync(tarefaDto);
                return Ok(tarefa);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
