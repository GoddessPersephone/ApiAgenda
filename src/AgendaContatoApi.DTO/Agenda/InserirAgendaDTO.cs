using AgendaContatoApi.DTO.Contato;
using AgendaContatoApi.DTO.Endereco;

namespace AgendaContatoApi.DTO.Agenda
{
    public class InserirAgendaDTO
    {
        public string? Nome { get; set; }
        public List<InserirContatoDTO>? Contatos { get; set; }
        public List<InserirEnderecoDTO>? Enderecos { get; set; }
    }
}