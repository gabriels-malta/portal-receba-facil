using RecebaFacil.Domain.Core.BaseEntities;
using RecebaFacil.Domain.Enums;
using System;

namespace RecebaFacil.Domain.Entities
{
    public class Contato : LoggableEntity
    {
        private string _valor;

        public Guid EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Valor
        {
            get { return FormatarValor(); }
            set { _valor = value; }
        }
        public TipoContato TipoContato { get; set; }
        public bool Ativo { get; set; }

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