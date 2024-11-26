using AgendaContatoApi.DTO.Contato;

namespace AgendaContatoApi.DTO.Agenda
{
    public class AlterarAgendaContatoDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }

        public AlterarContatoDTO contatoDTO { get; set; }
        //DTO Endereco
        //DTO Contato
    }
}