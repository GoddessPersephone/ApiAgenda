using AgendaContatoApi.DTO.Contato;
using AgendaContatoApi.Interface.Services;
using AgendaContatoApi.Model;

namespace AgendaContatoApi.Services.Contato
{
    public class ContatoService : IContatoService
    {
        public Task<List<ContatoModel>> Obter()
        {
            throw new NotImplementedException();
        }
        public Task<ContatoModel> ObterPorId(int id)
        {
            throw new NotImplementedException();
        }
        public Task<List<ContatoModel>> Inserir(List<InserirContatoDTO> liContato)
        {
            throw new NotImplementedException();
        }
        public Task<ContatoModel> Alterar(AlterarContatoDTO contato)
        {
            throw new NotImplementedException();
        }
        public Task<ContatoModel> Deletar(int id)
        {
            throw new NotImplementedException();
        }
    }
}