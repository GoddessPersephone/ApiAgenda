using AgendaContatoApi.Enumeradores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaContatoApi.Model
{
    [Table("Contato")]
    public class ContatoModel : BaseModel
    {
        [Key]
        public int Id { get; private set; }
        public string? Contato { get; private set; }
        public string? Observacao { get; private set; }
        public eTipoContato idTipoContato { get; private set; }
        public bool? FlagWhatsapp { get; private set; } = false;

        public ContatoModel()
        {
            Id = 0;
            Contato = string.Empty;
            Observacao = string.Empty;
            idTipoContato = eTipoContato.NENHUM;
            FlagWhatsapp = false;
        }  
        public ContatoModel(int id
                           ,string contato
                           ,string obs
                           ,eTipoContato tipoContato
                           ,bool? wpp )
        {
            Id = id;
            Contato = contato;
            Observacao = obs;
            idTipoContato = tipoContato;
            FlagWhatsapp = wpp;
        }
        public ContatoModel(string contato
                           ,string obs
                           ,eTipoContato tipoContato
                           ,bool? wpp )
        {
            Contato = contato;
            Observacao = obs;
            idTipoContato = tipoContato;
            FlagWhatsapp = wpp;
        }
    }
}