using AgendaContatoApi.Enumeradores;

namespace AgendaContatoApi.DTO.Endereco
{
    public class AlterarEnderecoDTO
    {
        public int IdEndereco { get; private set; }
        public string? Endereco { get; private set; }
        public string? Numero { get; private set; }
        public string? Observacao { get; private set; }
        public eTipoEndereco? TipoEndereco { get; private set; }
    }
}