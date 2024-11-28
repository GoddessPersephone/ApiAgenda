namespace AgendaContatoApi.DTO.Agenda
{
    public class AlterarAgendaDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Contato { get; private set; }
        public string? Endereco { get; private set; }
    }
}