using AgendaContatoApi.Ferramentas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AgendaContatoApi.Model
{
    [Table("Agenda")]
    public class AgendaModel : BaseModel
    {
        [Key]
        public int IdRegistroAgenda { get; private set; }
        public string Nome { get; private set; }
        public int[] idContato { get; private set; }
        public int[] idEndereco { get; private set; }

        public AgendaModel()
        {
            IdRegistroAgenda = 0;
            Nome = string.Empty;
            idContato = Array.Empty<int>();
            idEndereco = Array.Empty<int>();
        }
        public AgendaModel(int id
                          , string nome
                          , int[] contatoID
                          , int[] enderecoID)
        {
            IdRegistroAgenda = id;
            Nome = nome.CaixaAlta().RemoveCaracteres();
            idContato = contatoID;
            idEndereco = enderecoID;
        }
        public AgendaModel(string nome)
        {
            Nome = nome.CaixaAlta().RemoveCaracteres();
            AtivaRegistro();
            DataAtual();
        }
        public AgendaModel(int id, string nome)
        {
            IdRegistroAgenda = id;
            Nome = nome.CaixaAlta().RemoveCaracteres();
        }

        public void Alterar(string nome, int[] contato, int[] endereco)
        {
            Nome = nome.CaixaAlta().RemoveCaracteres();
            idContato = contato;
            idEndereco = endereco;
        }
    }
}