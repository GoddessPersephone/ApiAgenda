using AgendaContatoApi.DTO;
using AgendaContatoApi.Model;

namespace AgendaContatoApi.Interface
{
    public interface IAgendaContatoService
    {
        Task<List<ContatoModel>> Obter();
        Task<ContatoModel> ObterPorId(int id);
        Task<List<ContatoModel>> Inserir(List<InserirAgendaContatoDTO> liContato);
        Task<ContatoModel> Alterar(AlterarAgendaContatoDTO contato);
        Task<ContatoModel> Deletar(int id);
    }
}