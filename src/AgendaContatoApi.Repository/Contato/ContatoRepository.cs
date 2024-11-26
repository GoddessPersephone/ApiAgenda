using AgendaContatoApi.Interface.Repositories;
using AgendaContatoApi.Model;

namespace AgendaContatoApi.Repository.Contato
{
    internal class ContatoRepository : IContatoRepository
    {
        public Task<List<ContatoModel>> ObterRegistroAsync()
        {
            throw new NotImplementedException();
        }
        public Task<ContatoModel> ObterRegistroPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<List<ContatoModel>> InserirRegistroAsync(List<ContatoModel> liModel)
        {
            throw new NotImplementedException();
        }
        public Task<ContatoModel> AlterarRegistroAsync(ContatoModel model)
        {
            throw new NotImplementedException();
        }
        public Task<ContatoModel> DeletarRegistroAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}