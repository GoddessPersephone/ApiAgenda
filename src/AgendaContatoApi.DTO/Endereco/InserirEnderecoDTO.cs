using AgendaContatoApi.Enumeradores;

namespace AgendaContatoApi.DTO.Endereco
{
    public class InserirEnderecoDTO
    {
        public string? Endereco { get; set; }
        public string? Numero { get; set; }
        public string? Observacao { get; set; }
        public eTipoEndereco? TipoEndereco { get; set; }
    }
}