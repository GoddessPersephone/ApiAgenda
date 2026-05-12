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
        public string Contato { get; private set; }
        public string Endereco { get; private set; }

        public AgendaModel()
        {
            Id = 0;
            Nome = string.Empty;
            Contato = string.Empty;
            Endereco = string.Empty;
        }
        public AgendaModel(int id
                          , string nome
                          , string contato
                          , string endereco)
        {
            Id = id;
            Nome = nome.CaixaAlta().RemoveCaracteres();
            Contato = contato;
            Endereco = endereco;
        }
        public AgendaModel(string nome, string contato, string endereco)
        {
            Nome = nome.CaixaAlta().RemoveCaracteres();
            Contato = contato.CaixaAlta().RemoveCaracteres();
            Endereco = endereco.CaixaAlta().RemoveCaracteres();
            AtivaRegistro();
            DataAtual();
        }
        public AgendaModel(int id, string nome)
        {
            Id = id;
            Nome = nome.CaixaAlta().RemoveCaracteres();
        }

        public void Alterar(string nome, string contato, string endereco)
        {
            Nome = nome.CaixaAlta().RemoveCaracteres();
            Contato = contato.TiraEspacos();
            Endereco = endereco.CaixaAlta().RemoveCaracteres();
        }
    }
}