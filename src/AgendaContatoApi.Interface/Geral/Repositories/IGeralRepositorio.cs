namespace AgendaContatoApi.Interface.Geral.Repositories
{
    public interface IGeralRepositorio<T>
    {
        Task<List<T>> ObterRegistroAsync();
        Task<T> ObterRegistroPorIdAsync(int id);
        Task<List<T>> InserirRegistroAsync(List<T> liModel);
        Task<T> AlterarRegistroAsync(T model);
        Task<T> DeletarRegistroAsync(int id);
    }
}