using RecebaFacil.Domain.Application.Extensions;
using RecebaFacil.Domain.Enums;
using System;

namespace RecebaFacil.Domain.Entities
{
    public class Empresa : EntityBase<int>
    {

        public virtual TipoEmpresa TipoEmpresa { get; private set; }
        public string RazaoSocial { get; private set; }
        public string NomeFantasia { get; private set; }        
        public string Cnpj
        {
            get { return _cnpj.FormatarCnpj(); }
            private set { _cnpj = value; }
        }
        private string _cnpj;
        public DateTime DataCadastro { get; private set; }
        public virtual Endereco Endereco { get; private set; }

        private Empresa() { }
        public Empresa(
            TipoEmpresa tipoEmpresa,
            string razaoSocial,
            string nomeFantasia,
            string cnpj)
        {
            TipoEmpresa = tipoEmpresa;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            Cnpj = cnpj;
        }

        public Empresa(TipoEmpresa tipoEmpresa,
            string razaoSocial,
            string nomeFantasia,
            string cnpj,
            DateTime dataCadastro)
        {
            TipoEmpresa = tipoEmpresa;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            Cnpj = cnpj;
            DataCadastro = dataCadastro;
        }


        public Empresa AdicionarEndereco(Endereco endereco)
        {
            Endereco = endereco;
            return this;
        }
    }
}
