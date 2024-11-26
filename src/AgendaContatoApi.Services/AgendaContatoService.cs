using AgendaContatoApi.DTO.Agenda;
using AgendaContatoApi.Interface;
using AgendaContatoApi.Model;
using Microsoft.Extensions.Logging;

namespace AgendaContatoApi.Services
{
    public class AgendaContatoService : IAgendaContatoService
    {
        private readonly IAgendaContatoRepository _repo;
        private readonly ILogger<AgendaContatoService> _logger;
        public string mensagem = string.Empty;
        public bool sucesso = true;

        public AgendaContatoService(IAgendaContatoRepository repo, ILogger<AgendaContatoService> logger)
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
                listaRetorno = await _repo.ObterContatosAsync();
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
                    modelRetorno = await _repo.ObterContatoPorIdAsync(id);
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
        public async Task<List<AgendaModel>> Inserir(List<InserirAgendaContatoDTO> liContatoDTO)
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
                    var listaContatos = liContatoDTO.Select(item => new AgendaModel(nome: item.Nome, endereco: item.Endereco, contatos: item.Contato)).ToList();

                    listaRetorno = await _repo.InserirContatoAsync(listaContatos);
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
        public async Task<AgendaModel> Alterar(AlterarAgendaContatoDTO contato)
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
                    var model = new AgendaModel(id: contato.Id, nome: contato.Nome, enderecoID: contato.Endereco, contatoID: contato.Contato);

                    modelRetorno = await _repo.AlterarContatoAsync(model);
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
                    modelErro.ErroMensagem = mensagem;
                    return modelErro;
                }
                if (sucesso)
                {
                    modelRetorno = await _repo.DeletarContatoAsync(id);
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