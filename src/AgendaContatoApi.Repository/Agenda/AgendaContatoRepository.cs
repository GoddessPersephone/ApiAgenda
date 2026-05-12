using AgendaContatoApi.Data;
using AgendaContatoApi.Interface.Repositories;
using AgendaContatoApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AgendaContatoApi.Repository.Agenda
{
    public class AgendaContatoRepository : IAgendaRepository
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

        public async Task<List<AgendaModel>> ObterRegistroAsync()
        {
            var listaErro = new List<AgendaModel>();
            try
            {
                return await _context.TabelaAgenda.AsNoTracking().Where(x => x.Ativo).ToListAsync();
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

        public async Task<AgendaModel> ObterRegistroPorIdAsync(int id)
        {
            var mensagem = string.Empty;
            var modelErro = new AgendaModel();
            try
            {
                return await _context.TabelaAgenda
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

        public async Task<List<AgendaModel>> InserirRegistroAsync(List<AgendaModel> liContato)
        {
            var listaErro = new List<AgendaModel>();
            using var transacao = _context.Database.BeginTransaction();
            try
            {
                await _context.TabelaAgenda.AddRangeAsync(liContato);
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

        public async Task<AgendaModel> AlterarRegistroAsync(AgendaModel contato)
        {
            var modelErro = new AgendaModel();
            using var transacao = _context.Database.BeginTransaction();
            try
            {
                _context.TabelaAgenda.Update(contato);
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

        public async Task<AgendaModel> DeletarRegistroAsync(int id)
        {
            var modelErro = new AgendaModel();
            using var transacao = _context.Database.BeginTransaction();
            try
            {
                var registro = await _context.TabelaAgenda.FindAsync(id);

                if (registro is null)
                {
                    sucesso = false;
                    mensagem = "Contato não localizado no cadastro!";
                }
                else
                {
                    registro.InativaRegistro();
                    _context.TabelaAgenda.Update(registro);
                    await _context.SaveChangesAsync();

                    await transacao.CommitAsync();
                    _logger.LogInformation("Sucesso!");
                }
                modelErro.ErroMensagem = mensagem;
                return sucesso ? registro : modelErro;
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