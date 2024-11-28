namespace AgendaContatoApi.Interface.Geral.Services
{
    public interface IGeralService <T>
    {
        Task<T> Obter();
        Task<T> ObterPorId(int id);
        Task<T> Deletar(int id);
    }
}