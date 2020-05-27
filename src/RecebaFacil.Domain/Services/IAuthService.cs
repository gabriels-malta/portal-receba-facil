using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Domain.Services
{
    public interface IAuthService
    {
        int Autenticar(string email, string senha);
    }
}
