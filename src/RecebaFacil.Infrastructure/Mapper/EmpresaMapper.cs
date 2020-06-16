using Dapper.FluentMap.Mapping;
using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Infrastructure.Mapper
{
    internal class EmpresaMapper : EntityMap<Empresa>
    {
        internal EmpresaMapper()
        {
            Map(x => x.Id).ToColumn("intIdEmpresa");
            Map(x => x.RazaoSocial).ToColumn("vchRazaoSocial");
            Map(x => x.NomeFantasia).ToColumn("vchNomeFantasia");
            Map(x => x.Cnpj).ToColumn("vchCnpj");
            Map(x => x.DataCadastro).ToColumn("dtmDataCadastro");
            Map(x => x.TipoEmpresa).ToColumn("sinCodTipoEmpresa");
        }
    }
}
