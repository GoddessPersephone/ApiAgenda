namespace AgendaContatoApi.Model
{
    public class RetornoCompletoModel : BaseErroModel
    {
        public List<AgendaModel> Agenda { get; set; }
        public List<ContatoModel>? Contatos { get; set; }
        public List<EnderecoModel>? Enderecos { get; set; }
    }
}