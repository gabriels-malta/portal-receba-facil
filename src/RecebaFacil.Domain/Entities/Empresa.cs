using RecebaFacil.Domain.Application.Extensions;
using RecebaFacil.Domain.Enums;
using System;

namespace RecebaFacil.Domain.Entities
{
    public abstract class Empresa : EntityBase<int>
    {
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        private string _cnpj;

        public string Cnpj
        {
            get { return _cnpj.FormatarCnpj(); }
            set { _cnpj = value; }
        }


        public DateTime DataCadastro { get; set; }

        public abstract TipoEmpresa TipoEmpresa { get; }


        public virtual Endereco Endereco { get; private set; }

        public Empresa AdicionarEndereco(Endereco endereco)
        {
            Endereco = endereco;
            return this;
        }
    }
}
