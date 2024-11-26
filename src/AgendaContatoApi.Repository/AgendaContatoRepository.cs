using AgendaContatoApi.Data;
using AgendaContatoApi.Interface;
using AgendaContatoApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AgendaContatoApi.Repository
{
    public class AgendaContatoRepository : IAgendaContatoRepository
    {
        private readonly AgendaContext _context;
        private readonly ILogger<AgendaContatoRepository> _logger;
        public bool sucesso = true;
        public string mensagem = string.Empty;

        public AgendaContatoRepository(AgendaContext context, ILogger<AgendaContatoRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<AgendaModel>> ObterContatosAsync()
        {
            var listaErro = new List<AgendaModel>();
            try
            {
                return await _context.TabelaContatos.AsNoTracking().Where(x => x.Ativo).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Metodo em execucao => {MethodBase.GetCurrentMethod().Name}");
                _logger.LogError(ex, ex.Message);
                _logger.LogError(ex.StackTrace);
#if DEBUG
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                _logger.LogError($"Linha do erro: {trace.GetFrame(0).GetFileLineNumber()}");
#endif
                mensagem = $"Erro: {ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                listaErro.Add(new AgendaModel { ErroMensagem = mensagem });
                return listaErro;
            }
        }

        public async Task<AgendaModel> ObterContatoPorIdAsync(int id)
        {
            var mensagem = string.Empty;
            var modelErro = new AgendaModel();
            try
            {
                return await _context.TabelaContatos
                                     .Where(x => x.Ativo && x.Id == id)
                                     .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Metodo em execucao => {MethodBase.GetCurrentMethod().Name}");
                _logger.LogError(ex, ex.Message);
                _logger.LogError(ex.StackTrace);
#if DEBUG
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                _logger.LogError($"Linha do erro: {trace.GetFrame(0).GetFileLineNumber()}");
#endif
                mensagem += $"Erro: {ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                modelErro.ErroMensagem = mensagem;
                return modelErro;
            }
        }

        public async Task<List<AgendaModel>> InserirContatoAsync(List<AgendaModel> liContato)
        {
            var listaErro = new List<AgendaModel>();
            using var transacao = _context.Database.BeginTransaction();
            try
            {
                await _context.TabelaContatos.AddRangeAsync(liContato);
                await _context.SaveChangesAsync();

                await transacao.CommitAsync();
                _logger.LogInformation("Sucesso!");

                return liContato;
            }
            catch (Exception ex)
            {
                transacao.Rollback();
                _logger.LogError($"Metodo em execucao => {MethodBase.GetCurrentMethod().Name}");
                _logger.LogError(ex, ex.Message);
                _logger.LogError(ex.StackTrace);
#if DEBUG
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                _logger.LogError($"Linha do erro: {trace.GetFrame(0).GetFileLineNumber()}");
#endif
                mensagem = $" - {ex}, {ex.Message}!";
                _logger.LogError($"Erro: {mensagem}");
                listaErro.Add(new AgendaModel { ErroMensagem = mensagem });
                return listaErro;
            }
        }

        public async Task<AgendaModel> AlterarContatoAsync(AgendaModel contato)
        {
            var modelErro = new AgendaModel();
            using var transacao = _context.Database.BeginTransaction();
            try
            {
                //

                _context.TabelaContatos.Update(contato);
                await _context.SaveChangesAsync();

                await transacao.CommitAsync();
                _logger.LogInformation("Sucesso!");
                return contato;
            }
            catch (Exception ex)
            {
                transacao.Rollback();
                _logger.LogError($"Metodo em execucao => {MethodBase.GetCurrentMethod().Name}");
                _logger.LogError(ex, ex.Message);
                _logger.LogError(ex.StackTrace);
#if DEBUG
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                _logger.LogError($"Linha do erro: {trace.GetFrame(0).GetFileLineNumber()}");
#endif
                mensagem = $" - {ex}, {ex.Message}!";
                _logger.LogError($"Erro: {mensagem}");
                modelErro.ErroMensagem = mensagem;
                return modelErro;
            }
        }

        public async Task<AgendaModel> DeletarContatoAsync(int id)
        {
            var modelErro = new AgendaModel();
            using var transacao = _context.Database.BeginTransaction();
            try
            {
                var contato = await _context.TabelaContatos.FindAsync(id);

                if (contato is null)
                {
                    sucesso = false;
                    mensagem = "Contato não localizado no cadastro!";
                }
                else
                {
                    contato.InativaRegistro();
                    _context.TabelaContatos.Update(contato);
                    await _context.SaveChangesAsync();

                    await transacao.CommitAsync();
                    _logger.LogInformation("Sucesso!");
                }
                modelErro.ErroMensagem = mensagem;
                return sucesso ? contato : modelErro;

            }
            catch (Exception ex)
            {
                transacao.Rollback();
                _logger.LogError($"Metodo em execucao => {MethodBase.GetCurrentMethod().Name}");
                _logger.LogError(ex, ex.Message);
                _logger.LogError(ex.StackTrace);
#if DEBUG
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                _logger.LogError($"Linha do erro: {trace.GetFrame(0).GetFileLineNumber()}");
#endif
                mensagem = $" - {ex}, {ex.Message}!";
                _logger.LogError($"Erro: {mensagem}");
                modelErro.ErroMensagem = mensagem;
                return modelErro;
            }
        }
    }
}