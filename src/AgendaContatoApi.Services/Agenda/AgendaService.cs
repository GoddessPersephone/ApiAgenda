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

        public async Task<List<AgendaModel>> Obter()
        {
            var listaErro = new List<AgendaModel>();
            var listaRetorno = new List<AgendaModel>();
            try
            {
                listaRetorno = await _repo.ObterRegistroAsync();
                if (listaRetorno is not null && listaRetorno.Count > 0)
                {
                    if (!string.IsNullOrEmpty(listaRetorno[0].ErroMensagem))
                    {
                        sucesso = false;
                        mensagem += listaRetorno[0].ErroMensagem;
                        _logger.LogError("Erro: " + mensagem);
                    }
                }
                listaErro.Add(new AgendaModel { ErroMensagem = mensagem });
                return sucesso ? listaRetorno : listaErro;
            }
            catch (Exception ex)
            {
                mensagem += $"Erro: {ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                listaErro.Add(new AgendaModel { ErroMensagem = mensagem });
                return listaErro;
            }
        }
        public async Task<AgendaModel> ObterPorId(int id)
        {
            var modelErro = new AgendaModel();
            var modelRetorno = new AgendaModel();
            try
            {
                if (id <= 0)
                {
                    sucesso = false;
                    mensagem = "Informe um ID válido!";
                }
                if (sucesso)
                {
                    modelRetorno = await _repo.ObterRegistroPorIdAsync(id);
                    if (modelRetorno is not null)
                    {
                        if (!string.IsNullOrEmpty(modelRetorno.ErroMensagem))
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
        public async Task<List<AgendaModel>> Inserir(List<InserirAgendaDTO> liContatoDTO)
        {
            var listaErro = new List<AgendaModel>();
            var listaRetorno = new List<AgendaModel>();
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
                    var listaAgenda = liContatoDTO.Select(item => new AgendaModel(nome: item.Nome, contato: item.Contato, endereco: item.Endereco)).ToList();

                    listaRetorno = await _repo.InserirRegistroAsync(listaAgenda);
                    if (listaRetorno is not null && listaRetorno.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(listaRetorno[0].ErroMensagem))
                        {
                            sucesso = false;
                            mensagem += listaRetorno[0].ErroMensagem;
                            _logger.LogError("Erro: " + mensagem);
                        }
                    }
                }
                listaErro.Add(new AgendaModel { ErroMensagem = mensagem });
                return sucesso ? listaRetorno : listaErro;
            }
            catch (Exception ex)
            {
                mensagem += $"Erro: {ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                listaErro.Add(new AgendaModel { ErroMensagem = mensagem });
                return listaErro;
            }
        }
        public async Task<AgendaModel> Alterar(AlterarAgendaDTO contato)
        {
            var modelErro = new AgendaModel();
            var modelRetorno = new AgendaModel();
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

                    modelRetorno = await _repo.AlterarRegistroAsync(model);
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
                mensagem += $"Erro: {ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                modelErro.ErroMensagem = mensagem;
                return modelErro;
            }
        }

        public async Task<AgendaModel> Deletar(int id)
        {
            var modelErro = new AgendaModel();
            var modelRetorno = new AgendaModel();
            try
            {
                if (id <= 0)
                {
                    mensagem += $" - O campo Id é de preenchimento obrigatorio!";
                    _logger.LogError(mensagem);
                }
                if (sucesso)
                {
                    modelRetorno = await _repo.DeletarRegistroAsync(id);
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