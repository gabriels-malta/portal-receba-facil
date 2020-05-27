using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Enums;
using RecebaFacil.Domain.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RecebaFacil.Service.Mappers
{
    public class EmpresaMapper : IEmpresaMapper
    {
        public Empresa Map(DataRow row)
        {
            TipoEmpresa tipoEmpresa = (TipoEmpresa)row.Field<short>("sinCodTipoEmpresa");

            switch (tipoEmpresa)
            {
                case TipoEmpresa.PontoVenda:
                    return new PontoVenda
                    {
                        Id = row.Field<int>("intIdEmpresa"),
                        RazaoSocial = row.Field<string>("vchRazaoSocial"),
                        NomeFantasia = row.Field<string>("vchNomeFantasia"),
                        Cnpj = row.Field<string>("vchCnpj"),
                        DataCadastro = row.Field<DateTime>("dtmDataCadastro")
                    };
                case TipoEmpresa.PontoRetirada:
                    return new PontoRetirada
                    {
                        Id = row.Field<int>("intIdEmpresa"),
                        RazaoSocial = row.Field<string>("vchRazaoSocial"),
                        NomeFantasia = row.Field<string>("vchNomeFantasia"),
                        Cnpj = row.Field<string>("vchCnpj"),
                        DataCadastro = row.Field<DateTime>("dtmDataCadastro")
                    };
                default: return null;
            }
        }

        public IEnumerable<Empresa> Map(IEnumerable<DataRow> rows)
        {
            return rows.Select(x => Map(x));
        }
    }
}
