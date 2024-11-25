using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatoApi.Model
{
    public class ErroModel
    {
        [NotMapped]
        public string? ErroMensagem { get; set; }
    }
}
