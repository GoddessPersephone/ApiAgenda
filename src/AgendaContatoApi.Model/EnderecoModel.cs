using AgendaContatoApi.Enumeradores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaContatoApi.Model
{
    [Table("Endereco")]
    public class EnderecoModel : BaseModel
    {
        [Key]
        public int IdEndereco { get; private set; }
        public int IdProprietarioEndereco { get; private set; }
        public string? Endereco { get; private set; }
        public string? Numero { get; private set; }
        public string? Observacao { get; private set; }
        public eTipoEndereco? TipoEndereco { get; private set; }


        public EnderecoModel()
        {
            IdEndereco = 0;
            IdProprietarioEndereco = 0;
            Endereco = string.Empty;
            Numero = string.Empty;
            Observacao = string.Empty;
            TipoEndereco = eTipoEndereco.NENHUM;
        }
        public EnderecoModel(int enderecoID
                            , int proprietarioID
                            , string? endereco
                            , string? num
                            , string? obs
                            , eTipoEndereco tipoEndereco)
        {
            IdEndereco = enderecoID;
            IdProprietarioEndereco = proprietarioID;
            Endereco = endereco;
            Numero = num;
            Observacao = obs;
            TipoEndereco = tipoEndereco;
        }
        public EnderecoModel(int proprietarioID
                            , string? endereco
                            , string? num
                            , string? obs
                            , eTipoEndereco tipoEndereco)
        {
            IdProprietarioEndereco = proprietarioID;
            Endereco = endereco;
            Numero = num;
            Observacao = obs;
            TipoEndereco = tipoEndereco;
            AtivaRegistro();
            DataAtual();
        }
    }
}