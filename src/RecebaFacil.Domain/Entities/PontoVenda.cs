using RecebaFacil.Domain.Enums;

namespace RecebaFacil.Domain.Entities
{
    public class PontoVenda : Empresa
    {
        public override TipoEmpresa TipoEmpresa => TipoEmpresa.PontoVenda;

    }
}
