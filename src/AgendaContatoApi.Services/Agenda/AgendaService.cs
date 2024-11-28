using AgendaContatoApi.DTO.Agenda;
using AgendaContatoApi.Interface.Repositories;
using AgendaContatoApi.Interface.Services;
using AgendaContatoApi.Model;
using Microsoft.Extensions.Logging;

namespace AgendaContatoApi.Services.Agenda
{
    public class AgendaService : IAgendaService
    {
        private readonly IAgendaRepository _repo;
        private readonly ILogger<AgendaService> _logger;
        public string mensagem = string.Empty;
        public bool sucesso = true;

        public AgendaService(IAgendaRepository repo, ILogger<AgendaService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<RetornoCompletoModel> Obter()
        {
            var listaErro = new RetornoCompletoModel();
            var listaRetorno = new RetornoCompletoModel();
            try
            {
                listaRetorno.Agenda = await _repo.ObterRegistroAsync();
                if (listaRetorno is not null && listaRetorno.Agenda.Count > 0)
                {
                    if (!string.IsNullOrEmpty(listaRetorno.Agenda[0].ErroMensagem))
                    {
                        sucesso = false;
                        mensagem += listaRetorno.Agenda[0].ErroMensagem;
                        _logger.LogError("Erro: " + mensagem);
                    }
                }
                listaErro.ErroMensagem = mensagem;
                return sucesso ? listaRetorno : listaErro;
            }
            catch (Exception ex)
            {
                mensagem += $"Erro: {ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                listaErro.ErroMensagem = mensagem;
                return listaErro;
            }
        }
        public async Task<RetornoCompletoModel> ObterPorId(int id)
        {
            var modelErro = new RetornoCompletoModel();
            var modelRetorno = new RetornoCompletoModel();
            try
            {
                if (id <= 0)
                {
                    sucesso = false;
                    mensagem = "Informe um ID válido!";
                }
                if (sucesso)
                {
                    modelRetorno.Agenda[0] = await _repo.ObterRegistroPorIdAsync(id);
                    if (modelRetorno is not null)
                    {
                        if (!string.IsNullOrEmpty(modelRetorno.Agenda[0].ErroMensagem))
                        {
                            sucesso = false;
                            mensagem += modelRetorno.ErroMensagem;
                            _logger.LogError("Erro: " + mensagem);
                        }
                    }
                }
                modelErro.ErroMensagem = mensagem;
                return sucesso ? modelRetorno : modelErro;
            }
            catch (Exception ex)
            {
                mensagem += $"Erro: {ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                modelErro.ErroMensagem = mensagem;
                return modelErro;
            }
        }
        public async Task<RetornoCompletoModel> Inserir(List<InserirAgendaDTO> liContatoDTO)
        {
            var listaErro = new RetornoCompletoModel();
            var listaRetorno = new RetornoCompletoModel();
            try
            {
                #region Validações 
                if (liContatoDTO.Count() <= 0 && !liContatoDTO.Any())
                {
                    sucesso = false;
                    mensagem += " - Os campos são de preenchimento obrigatório!";
                    _logger.LogError(mensagem);
                }
                #endregion
                if (sucesso)
                {
                    var listaAgenda = liContatoDTO.Select(item => new AgendaModel(nome: item.Nome)).ToList();

                    listaRetorno.Agenda = await _repo.InserirRegistroAsync(listaAgenda);
                    if (listaRetorno is not null && listaRetorno.Agenda.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(listaRetorno.Agenda[0].ErroMensagem))
                        {
                            sucesso = false;
                            mensagem += listaRetorno.Agenda[0].ErroMensagem;
                            _logger.LogError("Erro: " + mensagem);
                        }
                    }
                }
                listaErro.ErroMensagem = mensagem;
                return sucesso ? listaRetorno : listaErro;
            }
            catch (Exception ex)
            {
                mensagem += $"Erro: {ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                listaErro.ErroMensagem = mensagem;
                return listaErro;
            }
        }
        public async Task<RetornoCompletoModel> Alterar(AlterarAgendaDTO contato)
        {
            var modelErro = new RetornoCompletoModel();
            var modelRetorno = new RetornoCompletoModel();
            try
            {
                #region Validações                
                if (contato.Id == 0)
                {
                    sucesso = false;
                    mensagem += " - O campo ID é de preenchimento obrigatório!";
                    _logger.LogError(mensagem);
                }
                #endregion
                if (sucesso)
                {
                    var model = new AgendaModel(id: contato.Id, nome: contato.Nome);

                    modelRetorno.Agenda[0] = await _repo.AlterarRegistroAsync(model);
                    if (modelRetorno is not null && !string.IsNullOrEmpty(modelRetorno.Agenda[0].ErroMensagem))
                    {
                        sucesso = false;
                        mensagem += $" - {modelRetorno.Agenda[0].ErroMensagem} !";
                        _logger.LogError(mensagem);
                    }
                }

                modelErro.ErroMensagem = mensagem;
                return sucesso ? modelRetorno : modelErro;
            }
            catch (Exception ex)
            {
                mensagem += $"Erro: {ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                modelErro.ErroMensagem = mensagem;
                return modelErro;
            }
        }

        public async Task<RetornoCompletoModel> Deletar(int id)
        {
            var modelErro = new RetornoCompletoModel();
            var modelRetorno = new RetornoCompletoModel();
            try
            {
                if (id <= 0)
                {
                    mensagem += $" - O campo Id é de preenchimento obrigatorio!";
                    _logger.LogError(mensagem);
                }
                if (sucesso)
                {
                    modelRetorno.Agenda[0] = await _repo.DeletarRegistroAsync(id);
                    if (modelRetorno is not null && !string.IsNullOrEmpty(modelRetorno.ErroMensagem))
                    {
                        sucesso = false;
                        mensagem += $" - {modelRetorno.ErroMensagem} !";
                        _logger.LogError(mensagem);
                    }
                }
                modelErro.ErroMensagem = mensagem;
                return sucesso ? modelRetorno : modelErro;
            }
            catch (Exception ex)
            {
                mensagem += $"Erro: {ex}, {ex.Message}";
                _logger.LogError(mensagem);
                modelErro.ErroMensagem = mensagem;
                return modelErro;
            }
        }
    }
}