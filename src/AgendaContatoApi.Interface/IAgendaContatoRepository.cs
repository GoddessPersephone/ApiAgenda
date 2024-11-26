using AgendaContatoApi.Model;

namespace AgendaContatoApi.Interface
{
    public interface IAgendaContatoRepository
    {
        Task<List<AgendaModel>> ObterContatosAsync();
        Task<AgendaModel> ObterContatoPorIdAsync(int id);
        Task<List<AgendaModel>> InserirContatoAsync(List<AgendaModel> liContato);
        Task<AgendaModel> AlterarContatoAsync(AgendaModel contato);
        Task<AgendaModel> DeletarContatoAsync(int id);
    }
}