using AgendaContatoApi.Ferramentas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AgendaContatoApi.Model
{
    [Table("Agenda")]
    public class AgendaModel : BaseModel
    {
        [Key]
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public int[] idContato { get; private set; }
        public int[] idEndereco { get; private set; }

        public AgendaModel()
        {
            Id = 0;
            idContato = Array.Empty<int>();
            idEndereco = Array.Empty<int>();
            Nome = string.Empty;
        }
        public AgendaModel(int id
                          , string nome
                          , int[] contatoID
                          , int[] enderecoID)
        {
            Id = id;
            idContato = contatoID;
            idEndereco = enderecoID;
            Nome = nome.CaixaAlta().RemoveCaracteres();
        }
        public AgendaModel(string nome)
        {
            Nome = nome.CaixaAlta().RemoveCaracteres();
            AtivaRegistro();
            DataAtual();
        }

        public void Alterar(string nome, int[] contato, int[] endereco)
        {
            Nome = nome.CaixaAlta().RemoveCaracteres();
            idContato = contato;
            idEndereco = endereco;
        }
    }
}