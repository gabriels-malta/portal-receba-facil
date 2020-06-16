using RecebaFacil.Domain.Enums;
using System;

namespace RecebaFacil.Domain.Entities
{
    public class PontoVenda : Empresa
    {
        public PontoVenda(string razaoSocial,
            string nomeFantasia,
            string cnpj)
            : base(TipoEmpresa.PontoVenda, razaoSocial, nomeFantasia, cnpj)
        { }

        public PontoVenda(string razaoSocial,
            string nomeFantasia,
            string cnpj,
            DateTime dataCadastro)
            : base(TipoEmpresa.PontoVenda, razaoSocial, nomeFantasia, cnpj, dataCadastro)
        { }
    }
}
