using RecebaFacil.Domain.Application.Extensions;
using RecebaFacil.Domain.Enums;

namespace RecebaFacil.Domain.Entities
{
    public class Contato : EntityBase<int>
    {
        private string _valor;

        public int EmpresaID { get; set; }
        public string Nome { get; set; }
        public string Valor
        {
            get { return FormatarValor(); }
            set { _valor = value; }
        }
        public TipoContato TipoContato { get; set; }
        public bool Ativo { get; set; }

        public virtual Empresa Empresa { get; private set; }
        public Contato AdicionarEmpresa(Empresa empresa)
        {
            Empresa = empresa;
            return this;
        }
        private string FormatarValor()
        {
            switch (TipoContato)
            {
                case TipoContato.Celular: return _valor.FormatarCelular();
                case TipoContato.Fax:
                case TipoContato.TelefoneFixo: return _valor.FormatarTelefoneFixo();
                case TipoContato.Site:
                case TipoContato.URA:
                case TipoContato.Outro:
                case TipoContato.Email:
                default:
                    return _valor;
            }
        }
    }
}