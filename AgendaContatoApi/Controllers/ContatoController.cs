using AgendaContatoApi.DTO.Contato;
using AgendaContatoApi.Interface.Services;
using AgendaContatoApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace AgendaContatoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _service;
        private readonly ILogger<AgendaController> _logger;
        public string mensagem = string.Empty;

        public ContatoController(IContatoService service, ILogger<AgendaController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContatoModel>>> ObterContatos()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    mensagem += "Erro: Modelo de informação incorreto!";
                    _logger.LogError(mensagem);
                    return BadRequest(mensagem);
                }
                var liContatos = await _service.Obter();
                if (liContatos is not null && liContatos.Count > 0)
                {
                    if (!string.IsNullOrEmpty(liContatos[0].ErroMensagem))
                    {
                        mensagem += liContatos[0].ErroMensagem;
                        _logger.LogError(mensagem);
                        return BadRequest(mensagem);
                    }
                    mensagem += "Sucesso!";
                    _logger.LogInformation(mensagem);
                    return Ok(liContatos);
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

                var contato = await _service.ObterPorId(id);

                if (contato is not null)
                {
                    if (!string.IsNullOrEmpty(contato.ErroMensagem))
                    {
                        mensagem += contato.ErroMensagem;
                        _logger.LogError(mensagem);
                        return BadRequest(mensagem);
                    }
                    mensagem += "Sucesso!";
                    _logger.LogInformation(mensagem);
                    return Ok(contato);
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

        [HttpPut("Contato")]
        public async Task<IActionResult> AlterarContato(AlterarContatoDTO contato)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    mensagem += "Erro: Modelo de informação incorreto!";
                    _logger.LogError(mensagem);
                    return BadRequest(mensagem);
                }
                var modelAlterada = await _service.Alterar(contato);
                if (modelAlterada is not null)
                {
                    if (!string.IsNullOrEmpty(modelAlterada.ErroMensagem))
                    {
                        mensagem += modelAlterada.ErroMensagem;
                        _logger.LogError(mensagem);
                        return BadRequest(mensagem);
                    }
                    mensagem += "Sucesso!";
                    _logger.LogInformation(mensagem);
                    return Ok(modelAlterada);
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