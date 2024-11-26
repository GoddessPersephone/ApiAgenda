using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaContatoApi.Model
{
    public class BaseErroModel
    {
        [NotMapped]
        public string? ErroMensagem { get; set; }
    }
}