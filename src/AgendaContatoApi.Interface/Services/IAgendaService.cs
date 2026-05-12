using AgendaContatoApi.DTO.Agenda;
using AgendaContatoApi.Interface.Geral.Services;
using AgendaContatoApi.Model;

namespace AgendaContatoApi.Interface.Services
{
    public interface IAgendaService : IGeralService<AgendaModel>
    {
        Task<List<AgendaModel>> Inserir(List<InserirAgendaDTO> liContato);
        Task<AgendaModel> Alterar(AlterarAgendaDTO contato);
    }
}