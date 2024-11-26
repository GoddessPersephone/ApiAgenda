using AgendaContatoApi.Enumeradores;

namespace AgendaContatoApi.DTO.Contato
{
    public class InserirContatoDTO
    {
        public string? Contato { get; set; }
        public string? Observacao { get; set; }
        public eTipoContato idTipoContato { get; set; }
        public bool? FlagWhatsapp { get; set; } = false;
    }
}