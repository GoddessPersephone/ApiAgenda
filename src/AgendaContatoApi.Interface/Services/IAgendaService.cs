using AgendaContatoApi.DTO.Agenda;
using AgendaContatoApi.Interface.Geral.Services;
using AgendaContatoApi.Model;

namespace AgendaContatoApi.Interface.Services
{
    public interface IAgendaService : IGeralService<RetornoCompletoModel>
    {
        Task<RetornoCompletoModel> Inserir(List<InserirAgendaDTO> liContato);
        Task<RetornoCompletoModel> Alterar(AlterarAgendaDTO contato);
    }
}