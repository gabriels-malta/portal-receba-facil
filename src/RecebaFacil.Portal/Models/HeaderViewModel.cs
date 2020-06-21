using RecebaFacil.Domain;
using RecebaFacil.Domain.Core.Model;

namespace RecebaFacil.Portal.Models
{
    public class HeaderViewModel
    {
        public HeaderViewModel(string tipoEmpresa, string empresa)
        {
            TipoEmpresa = tipoEmpresa;
            Empresa = empresa;
        }

        public string TipoEmpresa { get; }
        public string Empresa { get; }
        public string RotaMeuPerfil { get; private set; }

        public static HeaderViewModel Create(LoggedUser loggedUser)
        {
            string tipoEmpresa = loggedUser.Role == Roles.PONTO_RETIRADA ? "Ponto de Retirada" : "Ponto de Venda";            
            return new HeaderViewModel(tipoEmpresa, loggedUser.Empresa);
        }

        public void AdicionarRotaMeuPerfil(string rota)
        {
            RotaMeuPerfil = rota;
        }
    }
}
