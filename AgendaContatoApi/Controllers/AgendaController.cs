using AgendaContatoApi.DTO.Agenda;
using AgendaContatoApi.Interface.Services;
using AgendaContatoApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace AgendaContatoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaService _service;
        private readonly ILogger<AgendaController> _logger;
        public string mensagem = string.Empty;

        public AgendaController(IAgendaService service, ILogger<AgendaController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgendaModel>>> ObterContatos()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    mensagem += "Erro: Modelo de informação incorreto!";
                    _logger.LogError(mensagem);
                    return BadRequest(mensagem);
                }
                var liRegistro = await _service.Obter();
                if (liRegistro is not null && liRegistro.Count > 0)
                {
                    if (!string.IsNullOrEmpty(liRegistro[0].ErroMensagem))
                    {
                        mensagem += liRegistro[0].ErroMensagem;
                        _logger.LogError(mensagem);
                        return BadRequest(mensagem);
                    }
                    mensagem += "Sucesso!";
                    _logger.LogInformation(mensagem);
                    return Ok(liRegistro);
                }
                mensagem += "Dados não localizados!";

                _logger.LogError(mensagem);
                return NotFound(mensagem);
            }
            catch (Exception ex)
            {
                mensagem += $"{ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                return BadRequest(mensagem);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AgendaModel>> ObterContatoPorId(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    mensagem += "Erro: Modelo de informação incorreto!";
                    _logger.LogError(mensagem);
                    return BadRequest(mensagem);
                }

                var registro = await _service.ObterPorId(id);

                if (registro is not null)
                {
                    if (!string.IsNullOrEmpty(registro.ErroMensagem))
                    {
                        mensagem += registro.ErroMensagem;
                        _logger.LogError(mensagem);
                        return BadRequest(mensagem);
                    }
                    mensagem += "Sucesso!";
                    _logger.LogInformation(mensagem);
                    return Ok(registro);
                }
                mensagem += "Dados não localizados!";

                _logger.LogError(mensagem);
                return NotFound(mensagem);
            }
            catch (Exception ex)
            {
                mensagem += $"{ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                return BadRequest(mensagem);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<AgendaModel>>> InserirContatos(List<InserirAgendaDTO> registros)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    mensagem += "Erro: Modelo de informação incorreto!";
                    _logger.LogError(mensagem);
                    return BadRequest(mensagem);
                }
                var liRegistro = await _service.Inserir(registros);

                if (liRegistro is not null)
                {
                    if (!string.IsNullOrEmpty(liRegistro[0].ErroMensagem))
                    {
                        mensagem += liRegistro[0].ErroMensagem;
                        _logger.LogError(mensagem);
                        return BadRequest(mensagem);
                    }
                    mensagem += "Sucesso!";
                    _logger.LogInformation(mensagem);
                    return Ok(liRegistro);
                }
                mensagem += "Dados não localizados.";

                _logger.LogError(mensagem);
                return NotFound(mensagem);
            }
            catch (Exception ex)
            {
                mensagem += $"{ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                return BadRequest(mensagem);
            }
            //return CreatedAtAction(nameof(GetContato), new { id = liContato.Id }, liContato);
        }

        [HttpPut("Agenda")]
        public async Task<IActionResult> AlterarContato(AlterarAgendaDTO registro)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    mensagem += "Erro: Modelo de informação incorreto!";
                    _logger.LogError(mensagem);
                    return BadRequest(mensagem);
                }
                var modelRegistro = await _service.Alterar(registro);
                if (modelRegistro is not null)
                {
                    if (!string.IsNullOrEmpty(modelRegistro.ErroMensagem))
                    {
                        mensagem += modelRegistro.ErroMensagem;
                        _logger.LogError(mensagem);
                        return BadRequest(mensagem);
                    }
                    mensagem += "Sucesso!";
                    _logger.LogInformation(mensagem);
                    return Ok(modelRegistro);
                }
                mensagem += "Dados não localizados.";

                _logger.LogError(mensagem);
                return NotFound(mensagem);
            }
            catch (Exception ex)
            {
                mensagem += $"{ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                return BadRequest(mensagem);
            }
            /* if (id != contato.Id)
             {
                 return BadRequest();
             }

             await _repo.AlterarContatoAsync(contato);
             return NoContent();*/
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContato(int id)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    mensagem += "Erro: Modelo de informação incorreto!";
                    _logger.LogError(mensagem);
                    return BadRequest(mensagem);
                }


                var dadoExcluido = await _service.Deletar(id);

                if (dadoExcluido is not null)
                {
                    if (!string.IsNullOrEmpty(dadoExcluido.ErroMensagem))
                    {
                        mensagem += dadoExcluido.ErroMensagem;
                        _logger.LogError(mensagem);
                        return BadRequest(mensagem);
                    }
                    mensagem += "Sucesso!";
                    _logger.LogInformation(mensagem);
                    return Ok(mensagem);
                }

                mensagem += "Dados não localizados.";

                _logger.LogError(mensagem);
                return NotFound(mensagem);
            }
            catch (Exception ex)
            {
                mensagem += $"{ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                return BadRequest(mensagem);
            }
        }
    }
}