using AgendaContatoApi.Ferramentas;
using System.ComponentModel.DataAnnotations.Schema;
namespace AgendaContatoApi.Model
{
    [Table("Contato")]
    public class ContatoModel : BaseModel
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Endereco { get; private set; }
        public string[] Contato { get; private set; }

        public ContatoModel()
        {
            Id = 0;
            Nome = string.Empty;
            Endereco = string.Empty;
            Contato = Array.Empty<string>();
        }
        public ContatoModel(int id
                           , string nome
                           , string endereco
                           , string[] contatos)
        {
            Id = id;
            Nome = nome.CaixaAlta().RemoveCaracteres();
            Endereco = endereco.CaixaAlta().RemoveCaracteres();
            Contato = contatos;
        }
        public ContatoModel(string nome
                           , string endereco
                           , string[] contatos)
        {
            Nome = nome.CaixaAlta().RemoveCaracteres();
            Endereco = endereco.CaixaAlta().RemoveCaracteres();
            Contato = contatos;
            AtivaRegistro();
            DataAtual();
        }

        public void Alterar(string nome, string[] contatos, string endereco)
        {
            Nome = nome.CaixaAlta().RemoveCaracteres();
            Endereco = endereco.CaixaAlta().RemoveCaracteres();
            Contato = contatos;
        }
    }
}