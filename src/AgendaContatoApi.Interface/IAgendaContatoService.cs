using AgendaContatoApi.DTO.Agenda;
using AgendaContatoApi.Model;

namespace AgendaContatoApi.Interface
{
    public interface IAgendaContatoService
    {
        Task<List<AgendaModel>> Obter();
        Task<AgendaModel> ObterPorId(int id);
        Task<List<AgendaModel>> Inserir(List<InserirAgendaContatoDTO> liContato);
        Task<AgendaModel> Alterar(AlterarAgendaContatoDTO contato);
        Task<AgendaModel> Deletar(int id);
    }
}