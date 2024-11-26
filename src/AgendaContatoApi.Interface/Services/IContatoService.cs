using AgendaContatoApi.DTO.Contato;
using AgendaContatoApi.Interface.Geral.Services;
using AgendaContatoApi.Model;

namespace AgendaContatoApi.Interface.Services
{
    public interface IContatoService : IGeralService<ContatoModel>
    {
        Task<List<ContatoModel>> Inserir(List<InserirContatoDTO> liContato);
        Task<ContatoModel> Alterar(AlterarContatoDTO contato);
    }
}