using RecebaFacil.Domain.Core.BaseEntities;
using RecebaFacil.Domain.Enums;
using System.Collections.Generic;

namespace RecebaFacil.Domain.Entities
{
    public class Empresa : LoggableEntity
    {
        private string _cnpj;

        public virtual TipoEmpresa TipoEmpresa { get; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj
        {
            get { return _cnpj.FormatarCnpj(); }
            set { _cnpj = value; }
        }

        public virtual IList<Endereco> Enderecos { get; set; }
        public virtual IList<Contato> Contatos { get; set; }
        public virtual IList<Usuario> Usuarios { get; set; }
    }
}
