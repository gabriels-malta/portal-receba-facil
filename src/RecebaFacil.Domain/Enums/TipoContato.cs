using System.ComponentModel;

namespace RecebaFacil.Domain.Enums
{
    public enum TipoContato
    {
        [Description("E-mail")]
        Email = 1,

        [Description("Telefone")]
        TelefoneFixo = 2,

        [Description("Celular")]
        Celular = 3,

        [Description("Fax")]
        Fax = 4,

        [Description("SAC")]
        URA = 5,

        [Description("Site")]
        Site = 6,

        [Description("")]
        Outro = 255,
    }
}
