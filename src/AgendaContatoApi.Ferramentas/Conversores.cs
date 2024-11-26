using AgendaContatoApi.Enumeradores;

namespace AgendaContatoApi.Ferramentas
{
    public static partial class Geral
    {
        public static eTipoEndereco ConverteEnderecoEmEnum(this int valor)
        {
            switch (valor)
            {
                case 1:
                    return eTipoEndereco.RESIDENCIAL;
                case 2:
                    return eTipoEndereco.COMERCIAL;
            }
            return eTipoEndereco.RESIDENCIAL;
        }
        public static eTipoContato ConverteContatoEmEnum(this int valor)
        {
            switch (valor)
            {
                case 0:
                    return eTipoContato.NENHUM;
                case 1:
                    return eTipoContato.TELEFONE;
                case 2:
                    return eTipoContato.CELULAR;  
                case 3:
                    return eTipoContato.EMAIL; 
                case 4:
                    return eTipoContato.INSTAGRAM; 
                case 5:
                    return eTipoContato.LINKID;
                case 6:
                    return eTipoContato.FACEBOOK;
            }
            return eTipoContato.NENHUM;
        }
    }
}