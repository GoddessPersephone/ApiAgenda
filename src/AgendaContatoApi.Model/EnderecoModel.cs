using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaContatoApi.Model
{
    [Table ("Endereco")]
    public class EnderecoModel : BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}