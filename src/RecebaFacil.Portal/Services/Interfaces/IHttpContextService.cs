using System;
using System.Threading.Tasks;

namespace RecebaFacil.Portal.Services.Interfaces
{
    public interface IHttpContextService
    {
        string ObterRole { get; }

        int ObterIdUsuarioLogado();
        string ObterRotaInicial();

        Task SignIn(Guid usuarioId);
        Task SignOut();
    }
}