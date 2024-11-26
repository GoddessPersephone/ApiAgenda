using AgendaContatoApi.DTO.Contato;
using AgendaContatoApi.DTO.Endereco;

namespace AgendaContatoApi.DTO.Agenda
{
    public class AlterarAgendaDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }

        //public AlterarContatoDTO Contato { get; set; }
        //public AlterarEnderecoDTO Endereco { get; set; }
    }
}