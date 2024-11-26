namespace AgendaContatoApi.Model
{
    public class BaseModel : BaseErroModel
    {
        public bool Ativo { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public BaseModel()
        {
            Ativo = false;
            DataCadastro = DateTime.MinValue;
        }
        public void AtivaRegistro()
        {
            Ativo = true;
        }
        public void InativaRegistro()
        {
            Ativo = false;
        }
        public void DataAtual()
        {
            DataCadastro = DateTime.Now;
        }
        public void AtualizaDataCadastro(DateTime dataNova)
        {
            DataCadastro = dataNova;
        }
    }
}